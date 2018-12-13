using System;

namespace Assets.Scripts.Core
{
    public abstract class MonoBehaviourSingletonCapability<T> : StrictInitializedMonoBehaviour
    {
        public static T GetInstance()
        {
            return _instance;
        }

        public static void SetInstance(T instance)
        {
            _instance = instance;
        }

        protected override void CoreInitialize()
        {
            _instance = (T)Convert.ChangeType(this, typeof(T));
        }

        protected static T _instance;
    }
}
