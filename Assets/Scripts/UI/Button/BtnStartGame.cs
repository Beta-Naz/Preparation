using UnityEngine;
using System.Collections;
using Assets.Scripts.Core.Base;
using Assets.Scripts.Core.Manager;

namespace Assets.Scripts.UI.Button
{
	public class BtnStartGame: BaseButton
	{
        protected override void OnClick()
        {
            InvokeManager.IsStart = true;
        }
	}
}