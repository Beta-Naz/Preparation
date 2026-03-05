using Assets.Scripts.Core.Base;
using System;

namespace Assets.Scripts.UI.Button
{
    public class BtnResetSetting : BaseButton
    {
        protected override void OnClick()
        {
            SettingManager.Instance.ResetSetting();
        }
    }
}
