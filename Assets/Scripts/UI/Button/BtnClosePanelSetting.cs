using Assets.Scripts.Core.Base;

namespace Assets.Scripts.UI.Button
{
    public class BtnClosePanelSetting : BaseButton
    {
        protected override void OnClick()
        {
            ObjectManager.KeyValueGameObject["PanelSaveSettings"].SetActive(false);
            ObjectManager.KeyValueGameObject["PanelSetting"].SetActive(true);
            ObjectManager.KeyValueGameObject["PanelBLockForSetting"].SetActive(false);
        }
    }
}