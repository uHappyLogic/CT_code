using System.Collections.Generic;

namespace Assets.Scripts.Core
{
	public abstract class MonoBehaviourSingletonCapabilityManager<TS, T>
        : MonoBehaviourSingletonCapability<TS>
    {
        protected override void CoreInitialize()
        {
            base.CoreInitialize();

            _registered = new List<T>();
        }

        public void Add(T actor)
        {
            LogAddOperation(actor);
            _registered.Add(actor);
        }

        public abstract void LogAddOperation(T ctor);

        public List<T> GetRegistered()
        {
            return _registered;
        }

        abstract public string GetName();

        protected List<T> _registered;
    }
}