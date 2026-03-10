using Assets.Scripts.Core.Base;
using Assets.Scripts.Core.Manager;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Button
{
    public class BtnClickStart : BaseButton
    {
        protected override void OnClick()
        {
            if(LevelManager.Instance == null)
            {
                Debug.LogError("LevelManager.Instance == null", this);
                return;
            }
            LevelManager.Instance.UiLevelManager.HideAllPanels();
            if (InvokeManager.Instance == null)
            {
                Debug.LogError("InvokeManager.Instance == null", this);
                return;
            }
            InvokeManager.Instance.IsStart = true;
            InvokeManager.Instance.InvokeForKey("game");
        }
    }
}