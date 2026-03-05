using Assets.Scripts.Core.Base;
using Assets.Scripts.Core.Interface;
using UnityEngine;

public class DDLanguage : BaseDropDown, IInvoke
{
    [SerializeField] private string _invokeKey = "setting";
    public void Invoke()
    {
        string language = PlayerPrefs.GetString("Language", "en");
        if (SettingManager.Kostil) 
        {
            language = SettingManager.DefaultSettingData.DefaultSetting.Language; 
        }
        for (int i = 0; i < ThisDropdown.options.Count; i++)
        {
            if (ThisDropdown.options[i] == null)
            {
                continue;
            }
            if (ThisDropdown.options[i].text.ToLower()[0..2] == language)
            {
                ThisDropdown.value = i;
            }
        }
    }
    protected override void OnSelect(int index)
    {
        string value = ThisDropdown.options[index].text;
        SettingManager.RegisterSetting(IdDropDowm, value[0..2]);
    }
    protected override void Start()
    {
        base.Start();
        InvokeManager.Register(_invokeKey, this);
    }
}
