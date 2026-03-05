using UnityEngine;
using System.Collections;
using Assets.Scripts.Core.Base;

namespace Assets.Scripts.UI.Button
{
	public class BtnCloseFromSetting : BaseButton
	{
        protected override void OnClick()
        {
            if (SettingManager.IsUpdate)
            {
                ObjectManager.KeyValueGameObject["PanelSetting"].SetActive(false);
                ObjectManager.KeyValueGameObject["PanelSaveSettings"].SetActive(true);
                if(ObjectManager.KeyValueGameObject.ContainsKey("TMP_Timer"))
                {
                    GameObject timerText = ObjectManager.KeyValueGameObject["TMP_Timer"];
                    TmpTimer tmpTimer = timerText.GetComponent<TmpTimer>();
                    tmpTimer.UpdateTimer();
                }
            }
            else
            {
                ObjectManager.KeyValueGameObject["PanelSaveSettings"].SetActive(false);
                ObjectManager.KeyValueGameObject["PanelBLockForSetting"].SetActive(false);
            }
        }
	}
}