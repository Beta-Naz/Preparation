using Assets.Scripts.Core.Interface;
using Assets.Scripts.Core.Manager;
using Assets.Scripts.UI.Button;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Core.Base
{
	public abstract class BaseButton : MonoBehaviour
	{
		[SerializeField] protected string IdButton = "defaultButton";
        [SerializeField] protected bool IsClick = false;
        protected Button ThisButton;
        protected InvokeManager InvokeManager;
		protected SpriteManager SpriteManager;
        protected SettingManager SettingManager;
        protected ObjectManager ObjectManager;
        protected abstract void OnClick();
        protected virtual void Start()
		{
			InvokeManager = InvokeManager.Instance;
			if(InvokeManager == null)
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
            ThisButton = GetComponent<Button>();
            ThisButton.onClick.AddListener(OnClick);
        }
	}
}