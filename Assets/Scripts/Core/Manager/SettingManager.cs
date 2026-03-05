using Assets.Scripts.Core;
using Assets.Scripts.Core.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : BaseManager, IInitializable
{
    private bool _kostil = false;
    public bool Kostil
    {
        get 
        { 
            return _kostil; 
        } 
        set 
        { 
            _kostil = value;
            KostilMetod();
        }
    }
    private void KostilMetod()
    {
        if (InvokeManager.Instance != null)
        {
            InvokeManager.Instance.InvokeForKey("setting");
        }
        _kostil = false;
    }
    public static SettingManager Instance { get; private set; }
    private bool _isUpdate = false;
    public SettingDatas DefaultSettingData;
    public bool IsUpdate => _isUpdate;
    private Dictionary<string, object> _newSetting;
    public void Initialize()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if (DefaultSettingData != null)
        {
            DefaultSettingData.LoadFromJson();
        }
        Instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        _newSetting = new Dictionary<string, object>();
        Debug.Log($"менеджер SettingManager успешно инициализирован");
    }

    public void RegisterSetting(string key, object obj)
    {
        if(_newSetting == null)
        {
            Debug.LogWarning($"_newSetting == null");
            return;
        }
        if (!TrySetting(key, obj))
        {
            _newSetting[key] = obj;
            Debug.Log($"Настройка {key} добавлена в ожидание");
        }
        else
        {
            Unregister(key);
        }
        _isUpdate = _newSetting.Keys.Count > 0;
        return;
    }
    public void CancelSetting()
    {
        _newSetting = new Dictionary<string, object>();
        _isUpdate = false;
        Debug.Log($"Временные настройки отменены");
    }
    public void ResetSetting()
    {
        if (DefaultSettingData == null)
        {
            Debug.LogWarning($"DefaultSetting == null");
            return;
        }
        RegisterSetting("fullscreen", DefaultSettingData.DefaultSetting.FullScreen);
        RegisterSetting("music", DefaultSettingData.DefaultSetting.Music);
        RegisterSetting("sound", DefaultSettingData.DefaultSetting.Sound);
        RegisterSetting("resolution", $"{DefaultSettingData.DefaultSetting.Resolution.Width}" +
            $":{DefaultSettingData.DefaultSetting.Resolution.Height}");
        Kostil = true;
        Debug.Log($"Базовые настройки применены");
    }
    public void UpdateSetting()
    {
        foreach(var key in _newSetting.Keys)
        {
            AddSetting(key);
        }
        ApplySetting();
        _newSetting = new Dictionary<string, object>();
        Debug.Log($"Временные настройки применены");
    }
    private bool TrySetting(string key, object obj)
    {
        try
        {
            Debug.Log("Проверка пошла");
            bool isOldSetting = true;
            switch (key.ToLower())
            {
                case "fullscreen":
                    if (obj is bool _fullscreen)
                    {
                        bool fullscreen = PlayerPrefs.GetInt("Fullscreen") != 0;
                        if (_fullscreen != fullscreen)
                        {
                            isOldSetting = false;
                            Debug.Log($"Новая настройка!");
                        }
                    }
                    break;
                case "resolution":
                    if (obj is string resolutionString)
                    {
                        string[] mas = resolutionString.Trim().Split(':');
                        if (mas.Length == 2 && int.TryParse(mas[0], out int newWidth) && int.TryParse(mas[1], out int newHeight))
                        {
                            int width = PlayerPrefs.GetInt("Width");
                            int height = PlayerPrefs.GetInt("Height");
                            if (newWidth != width || newHeight != height)
                            {
                                isOldSetting = false;
                                Debug.Log($"Новая настройка!");
                            }
                        }
                    }
                    break;
                case "music":
                    if (obj is bool _musicTry)
                    {
                        bool musicTry = PlayerPrefs.GetInt("Music") != 0;
                        if (musicTry != _musicTry)
                        {
                            isOldSetting = false;
                            Debug.Log($"Новая настройка!");
                        }
                    }
                    break;
                case "sound":
                    if (obj is bool _soundsTry)
                    {
                        bool soundsTry = PlayerPrefs.GetInt("Sound") != 0;
                        if (soundsTry != _soundsTry)
                        {
                            isOldSetting = false;
                            Debug.Log($"Новая настройка!");
                        }
                    }
                    break;
                case "language":
                    if (obj is string _language)
                    {
                        string language = PlayerPrefs.GetString("Language");
                        if (language.Trim().ToLower() != _language.Trim().ToLower())
                        {
                            isOldSetting = false;
                            Debug.Log($"{language} != {language}");
                            Debug.Log($"Новая настройка!");
                        }
                    }
                    break;
            }
            Debug.Log($"ключ {key.ToLower()}");
            return isOldSetting;
        }
        catch (Exception e)
        {
            Debug.LogError(e, this);
            return false;
        }
    }
    private void AddSetting(string key)
    {
        Debug.Log($"ключ {key.ToLower()}");
        switch (key.ToLower())
        {
            case "fullscreen":
                if (_newSetting[key] is bool fullscreen)
                {
                    PlayerPrefs.SetInt("Fullscreen", fullscreen ? 1 : 0);
                }
                break;
            case "resolution":
                if (_newSetting[key] is string resolutionString)
                {
                    string[] mas = resolutionString.Trim().Split(':');
                    if(mas.Length == 2)
                    {
                        if(int.Parse(mas[0]) is int width && int.Parse(mas[1]) is int height)
                        {
                            PlayerPrefs.SetInt("Width", width);
                            PlayerPrefs.SetInt("Height", height);
                        }
                    }
                }
                break;
            case "music":
                if (_newSetting[key] is bool musicTry)
                {
                    PlayerPrefs.SetInt("Music", musicTry ? 1 : 0);
                }
                break;
            case "sound":
                if (_newSetting[key] is bool soundsTry)
                {
                    PlayerPrefs.SetInt("Sound", soundsTry ? 1 : 0);
                }
                break;
            case "language":
                if (_newSetting[key] is string language)
                {
                    PlayerPrefs.SetString("Language", language.ToLower());
                }
                break;
            default:
                Debug.LogWarning($"{key.ToLower()}", this);
                break;
        }
    }
    private void ApplySetting()
    {
        bool fullscreen = PlayerPrefs.GetInt("Fullscreen") != 0;
        bool musicTry = PlayerPrefs.GetInt("Music") != 0;
        bool soundTry = PlayerPrefs.GetInt("Sound") != 0;
        int width = PlayerPrefs.GetInt("Width");
        int height = PlayerPrefs.GetInt("Height");
        Screen.SetResolution(width, height, fullscreen);
        InvokeManager.Instance.InvokeForKey("music");
        InvokeManager.Instance.InvokeForKey("sound");

    }
    public override void Unregister(string key)
    {
        _newSetting.Remove(key);
        Debug.Log($"Настройка {key} убрана из ожидание");
    }
}
