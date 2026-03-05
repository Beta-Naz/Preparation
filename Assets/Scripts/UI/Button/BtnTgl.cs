using Assets.Scripts.Core.Base;
using Assets.Scripts.Core.Interface;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BtnTgl: BaseButton, IInvoke
{
    private Image _image;
    [SerializeField] private string _invokeKey = "setting";
    public void Invoke()
    {
        bool isClick;
        if (IdButton == "fullscreen")
        {
            isClick = PlayerPrefs.GetInt("Fullscreen", IsClick ? 1 : 0) != 0;
            if(SettingManager.Kostil)
            {
                isClick = SettingManager.Instance.DefaultSettingData.DefaultSetting.FullScreen;
            }
        }
        else if(IdButton == "music")
        {
            isClick = PlayerPrefs.GetInt("Music", IsClick ? 1 : 0) != 0;
            if (SettingManager.Kostil)
            {
                isClick = SettingManager.Instance.DefaultSettingData.DefaultSetting.Music;
            }
        }
        else if(IdButton == "sound")
        {
            isClick = PlayerPrefs.GetInt("Sound", IsClick ? 1 : 0) != 0;
            if (SettingManager.Kostil)
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
        SettingManager.RegisterSetting(IdButton, IsClick);
    }
    void ChoseSprite()
    {
        if(_image == null)
        {
            _image = GetComponent<Image>();
        }
        if (IdButton == "fullscreen")
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
