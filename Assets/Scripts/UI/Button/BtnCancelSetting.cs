using Assets.Scripts.Core.Base;

namespace Assets.Scripts.UI.Button
{
	public class BtnCancelSetting: BaseButton
	{
        protected override void OnClick()
        {
            SettingManager.CancelSetting();
        }
	}
}