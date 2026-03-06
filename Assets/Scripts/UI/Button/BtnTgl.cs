using Assets.Scripts.Core.Base;
using Assets.Scripts.Core.Enums;
using Assets.Scripts.Core.Interface;
using UnityEngine;
using UnityEngine.UI;

public class BtnTgl: BaseButton, IInvoke
{
    private Image _image;
    [SerializeField] private string _invokeKey = "setting";
    [SerializeField] private TypeSetting _settingKey = TypeSetting.sound;
    public void Invoke()
    {
        bool isClick;
        if (_settingKey == TypeSetting.fullscreen)
        {
            isClick = SettingManager.SettingStorage.LoadFullscreen();
            if (SettingManager.IsDefaultSetting)
            {
                isClick = SettingManager.Instance.DefaultSettingData.DefaultSetting.FullScreen;
            }
        }
        else if(_settingKey == TypeSetting.music)
        {
            isClick = SettingManager.SettingStorage.LoadMusic();
            if (SettingManager.IsDefaultSetting)
            {
                isClick = SettingManager.Instance.DefaultSettingData.DefaultSetting.Music;
            }
        }
        else if(_settingKey == TypeSetting.sound)
        {
            isClick = SettingManager.SettingStorage.LoadSound();
            if (SettingManager.IsDefaultSetting)
            {
                isClick = SettingManager.Instance.DefaultSettingData.DefaultSetting.Sound;
            }
        }
        else
        {
            return;
        }
        IsClick = isClick;
        ChoseSprite();
    }
    protected override void OnClick()
    {
        IsClick = !IsClick;
        ChoseSprite();
        RegisterSetting();
    }
    private void RegisterSetting()
    {
        SettingManager.RegisterSetting(_settingKey, IsClick);
    }
    void ChoseSprite()
    {
        if(_image == null)
        {
            _image = GetComponent<Image>();
        }
        if (_settingKey == TypeSetting.fullscreen)
        {
            _image.sprite = IsClick ?  SpriteManager.SpriteTglOff : SpriteManager.SpriteTglOn;
        }
        else
        {
            _image.sprite = IsClick ? SpriteManager.SpriteTglOn : SpriteManager.SpriteTglOff;
        }
    }
    protected override void Start()
    {
        base.Start();
        InvokeManager.Register(_invokeKey, this);
    }
}
