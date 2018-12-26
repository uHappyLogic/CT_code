using Assets.Scripts.GameUnits.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Gui;
using Mirror;

namespace Assets.Scripts.GameUnits
{
	public class IntSkeletonLogic : GameUnit
	{
		public new void Start()
		{
			base.Start();

			UnitsManager.GetInstance().Add(this);

			VisibleLogger.GetInstance().LogDebug(
				string.Format("Start [{0}]", GetId())
			);
	
			name = GetId();
		}

		public override void UpdateAliveGameUnit()
		{
			UpdateLogic();
			UpdateAnimation();
		}

		public override void UpdateDeadGameUnit()
		{
			if (Time.time - _timeOfDie < 2f)
				return;

			Transform.localPosition += Vector3.down * Time.deltaTime * 1f;

			if (Time.time - _timeOfDie > 8f)
				CmdChangeToWaitForDisposal();
		}

		[Command]
		public void CmdChangeToWaitForDisposal()
		{
			LifeState = UnitLifeState.WAITING_FOR_DISPOSAL;
		}

		public override void OnDeadAction()
		{
			LifeState = UnitLifeState.DEAD;

			VisibleLogger.GetInstance().LogDebug(
				string.Format("OnDeadAction [{0}]", GetId())
			);

			Animator.SetBool("IsAttacking", false);
			Animator.SetBool("IsDead", true);
			_timeOfDie = Time.time;
			Destroy(GetComponent<NavMeshAgent>());

			CmdChangeToDead();
		}

		[Command]
		public void CmdChangeToDead()
		{
			LifeState = UnitLifeState.DEAD;
		}

		private void UpdateLogic()
		{
			Duel.Defender = TargetSelector.GetInstance().GetNearestHostileUnit(this);

			if (Duel.Defender == null)
			{
				Animator.SetBool("IsAttacking", false);
				Animator.SetBool("IsRunning", false);

				SetTarget(Transform.localPosition);

				return;
			}

			if (IsInMeleeRangeOf(Duel.Defender))
			{
				Animator.SetBool("IsAttacking", true);
				Animator.SetBool("IsRunning", false);
			}
			else
			{
				SetTarget(Duel.Defender.Transform.localPosition);


				Animator.SetBool("IsAttacking", false);
			}

			RotateTowards(Duel.Defender.Transform);
		}

		private void SetTarget(Vector3 target)
		{
			NavMeshAgent.SetDestination(target);
			Animator.SetBool("IsRunning", true);
		}

		private void UpdateAnimation()
		{
			if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
				Animator.SetBool("IsRunning", false);
		}

		private bool IsInMeleeRangeOf(GameActor target)
		{
			float distance;

			if (target.Collider != null)
			{
				distance = Vector3.Distance(transform.position, target.Collider.ClosestPoint(transform.position));
			}
			else
			{
				distance = Vector3.Distance(transform.position, target.transform.position);
			}

			return distance < 5f;
		}

		private void RotateTowards(Transform target)
		{
			Vector3 direction = (target.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
		}

		private float _timeOfDie;
	}
}
