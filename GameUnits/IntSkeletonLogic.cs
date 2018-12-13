using Assets.Scripts.GameUnits.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.GameUnits
{
	public class IntSkeletonLogic : GameUnit
	{
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
				_canBeUnregistered = true;
		}

		public override void OnDeadAction()
		{
			Animator.SetBool("IsDead", true);
			Destroy(GetComponent<NavMeshAgent>());
			_timeOfDie = Time.time;
		}

		public override bool CanBeUnregistered()
		{
			return _canBeUnregistered;
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
				RotateTowards(Duel.Defender.Transform);

				Animator.SetBool("IsAttacking", false);
			}
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
				Debug.Log("<color=green>[Debug]</color> Collider available for distance measurement {" + GetId() + " : " + target.GetId() + "}");
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

		private bool _canBeUnregistered;
		private float _timeOfDie;
	}
}
