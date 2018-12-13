using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public abstract class SingletonCapability<T> : StrictInitialized
    {
        public static T GetInstance()
        {
            return _instance;
        }

        protected override void CoreInitialize()
        {
            _instance = (T)Convert.ChangeType(this, typeof(T));
        }

        protected static T _instance;
    }
}
