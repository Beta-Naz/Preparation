using Assets.Scripts.Core.Base;
using Assets.Scripts.Core.Interface;
using System;
using UnityEngine;

namespace Assets.Scripts.UI.Dropdown
{
	public class DDResolution : BaseDropDown, IInvoke
	{
        [SerializeField] private string _invokeKey = "setting";
        public void Invoke()
        {
            try
            {
                int width = PlayerPrefs.GetInt("Width", 1920);
                int height = PlayerPrefs.GetInt("Height", 1080);
                string resolution = $"{width}:{height}";
                if(SettingManager.Kostil) //Добавлено
                {
                    width = SettingManager.DefaultSettingData.DefaultSetting.Resolution.Width; //Добавлено
                    height = SettingManager.DefaultSettingData.DefaultSetting.Resolution.Height; //Добавлено
                    resolution = $"{width}:{height}"; //Добавлено
                }
                for (int i = 0; i < ThisDropdown.options.Count; i++) 
                {
                    if (ThisDropdown.options[i] == null)
                    {
                        continue;
                    }
                    if (ThisDropdown.options[i].text == resolution)
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
                SettingManager.RegisterSetting(IdDropDowm, value);
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