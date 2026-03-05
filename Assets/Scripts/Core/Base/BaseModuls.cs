using Assets.Scripts.Core.Interface;
using Assets.Scripts.Core.Manager;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.Base
{
	public abstract class BaseModuls: MonoBehaviour, IInvoke
	{
        [SerializeField] protected string KeyInvoke = "game";
        protected InvokeManager InvokeManager;
        protected SpriteManager SpriteManager;
        protected SettingManager SettingManager;
        protected ObjectManager ObjectManager;
        protected bool IsStart = false;
        public abstract void Invoke();
        protected virtual void Start()
        {
            InvokeManager = InvokeManager.Instance;
            if (InvokeManager == null)
            {
                Debug.LogWarning("InvokeManager не инициализирован", this);
            }
            SpriteManager = SpriteManager.Instance;
            if (SpriteManager == null)
            {
                Debug.LogWarning("SpriteManager не инициализирован", this);
            }
            SettingManager = SettingManager.Instance;
            if (SettingManager == null)
            {
                Debug.LogWarning("SettingManager не инициализирован", this);
            }
            ObjectManager = ObjectManager.Instance;
            if (ObjectManager == null)
            {
                Debug.LogWarning("ObjectManager не инициализирован", this);
            }
            InvokeManager.Register(KeyInvoke, this);
            Debug.Log($"Ключ: {KeyInvoke}", this);
        }
    }
}