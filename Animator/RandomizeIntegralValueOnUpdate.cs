using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class RandomizeIntegralValueOnUpdate : StateMachineBehaviour
    {
        public int MinInclusive;
        public int MaxExclusive;
        public string ParameterName;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetInteger(ParameterName, (int)Random.Range(MinInclusive, MaxExclusive - (0.01f * MaxExclusive)));
        }
    }
}
