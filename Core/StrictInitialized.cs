using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public abstract class  StrictInitialized
    {
        public void InitInOrder()
        {
            CoreInitialize();
            CustomInitialization();
        }

        protected abstract void CoreInitialize();
        protected abstract void CustomInitialization();
    }
}
