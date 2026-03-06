using Assets.Scripts.Core.Base;
using Assets.Scripts.Core.Enums;
using Assets.Scripts.Core.Interface;
using Assets.Scripts.Data.SettingsDatas;
using System;
using UnityEngine;

namespace Assets.Scripts.UI.Dropdown
{
	public class DDResolution : BaseDropDown, IInvoke
	{
        [SerializeField] private string _invokeKey = "setting";
        [SerializeField] private TypeSetting _settingKey = TypeSetting.resolution;
        public void Invoke()
        {
            try
            {
                ResolutionData resolution = SettingManager.SettingStorage.LoadResolution();
                if(SettingManager.IsDefaultSetting) //Добавлено
                {
                    resolution = SettingManager.DefaultSettingData.DefaultSetting.Resolution;
                }
                string resolutionString = $"{resolution.Width}:{resolution.Height}";
                for (int i = 0; i < ThisDropdown.options.Count; i++) 
                {
                    if (ThisDropdown.options[i] == null)
                    {
                        continue;
                    }
                    if (ThisDropdown.options[i].text == resolutionString)
                    {
                        ThisDropdown.value = i;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e,this);
            }
        }
        protected override void OnSelect(int index)
        {
            try
            {
                string value = ThisDropdown.options[index].text;
                SettingManager.RegisterSetting(_settingKey, value);
                Debug.Log("Меня кликнули!");
            }
            catch (Exception e)
            {
                Debug.LogError(e, this);
            }
        }
        protected override void Start()
        {
            base.Start();
            InvokeManager.Register(_invokeKey, this);
        }
    }
}
