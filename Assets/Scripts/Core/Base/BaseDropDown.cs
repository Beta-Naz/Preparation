using Assets.Scripts.Core.Interface;
using Assets.Scripts.Core.Manager;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Core.Base
{
	public abstract class BaseDropDown: MonoBehaviour
    {

        [SerializeField] protected string IdDropDowm = "defaultDropDown";
        protected TMP_Dropdown ThisDropdown;
        protected InvokeManager InvokeManager;
        protected SpriteManager SpriteManager;
        protected SettingManager SettingManager;
        protected ObjectManager ObjectManager;
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
            ThisDropdown = GetComponent<TMP_Dropdown>();
            ThisDropdown.onValueChanged.AddListener(OnSelect);
        }
        protected abstract void OnSelect(int index);
    }
}