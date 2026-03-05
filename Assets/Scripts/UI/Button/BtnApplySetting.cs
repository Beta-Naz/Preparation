using UnityEngine;
using System.Collections;
using Assets.Scripts.Core.Base;

namespace Assets.Scripts.UI.Button
{
    public class BtnApplySetting : BaseButton
    {
        protected override void OnClick()
        {
            SettingManager.UpdateSetting();
        }
        protected override void Start()
        {
            base.Start();
        }
    }
}