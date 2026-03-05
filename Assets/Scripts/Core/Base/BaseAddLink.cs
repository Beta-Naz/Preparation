using UnityEngine;
using System.Collections;
using Assets.Scripts.Core.Interface;

namespace Assets.Scripts.Core.Base
{
	public abstract class BaseAddLink : MonoBehaviour, IInitializable
    {
        protected string Key = "defaultLink";
        protected ObjectManager ObjectManager;
        public void Initialize()
        {
            ObjectManager = ObjectManager.Instance;
            Key = gameObject.name;
            if (ObjectManager == null)
            {
                Debug.LogWarning("ObjectManager не инициализирован", this);
                return;
            }
            Key = ObjectManager.Register(Key, gameObject);
            DebugMessage();
        }
        void Awake()
        {
            Initialize();
        }
        protected abstract void DebugMessage();
    }
}