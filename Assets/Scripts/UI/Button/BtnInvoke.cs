using UnityEngine;
using Assets.Scripts.Core.Base;

namespace Assets.Scripts.UI.Button
{
	public class BtnInvoke : BaseButton
	{
        [SerializeField] private string _invokeKey;
        protected override void OnClick()
        {
            if (string.IsNullOrEmpty(_invokeKey))
            {
                Debug.LogWarning("Ключ уведомления не назначен", this);
                return;
            }
            InvokeManager.InvokeForKey(_invokeKey);
        }
	}
}