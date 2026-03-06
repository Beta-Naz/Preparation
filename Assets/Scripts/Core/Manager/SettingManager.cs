using Assets.Scripts.Core;
using Assets.Scripts.Core.Classes.Setting;
using Assets.Scripts.Core.Enums;
using Assets.Scripts.Core.Interface;
using Assets.Scripts.Data.SettingsDatas;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour, IInitializable
{
    private bool _isDefaultSetting = false;
    public bool IsDefaultSetting
    {
        get 
        { 
            return _isDefaultSetting; 
        } 
        set 
        { 
            _isDefaultSetting = value;
            IsDefaultSettingMetod();
        }
    }
    /// <summary>
    /// Этот метод позволяет изменить вид у кнопок на базовый, то есть 
    /// мы переиспользуем IInvoke, чтобы не создавать допольнительный интерфейс.
    /// Логика в том, что в уведомляются все методы и там есть проверка IsDefaultSetting
    /// Если да, то применяются дефолтные настройки и позже IsDefoltSetting снова становиться false
    /// </summary>
    private void IsDefaultSettingMetod()
    {
        if (InvokeManager.Instance != null)
        {
            InvokeManager.Instance.InvokeForKey("setting");
        }
        _isDefaultSetting = false;
    }
    public static SettingManager Instance { get; private set; }
    private bool _isUpdate = false;
    public bool IsUpdate => _isUpdate;
    public SettingDatas DefaultSettingData;
    public SettingStorage SettingStorage;
    private Dictionary<TypeSetting, object> _newSetting;
    /// <summary>
    /// Метод для инициализации, у нас существует в проекте IntializedManager, который инициализирует 
    /// все менеджеры по порядку, чтобы не было исключений 
    /// </summary>
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
        _newSetting = new Dictionary<TypeSetting, object>();
        SettingStorage = new SettingStorage();
        Debug.Log($"менеджер SettingManager успешно инициализирован");
    }
    /// <summary>
    /// Регистрация настроек, если настроека уже используется, то она удаляется.
    /// </summary>
    /// <param name="key">Ключ настройки</param>
    /// <param name="obj">Значение настройки</param>
    public void RegisterSetting(TypeSetting key, object obj)
    {
        if (_newSetting == null) return;
        if (IsNewSetting(key, obj)) _newSetting[key] = obj;
        else UnregisterNewSetting(key);
        _isUpdate = _newSetting.Keys.Count > 0;
        return;
    }
    /// <summary>
    /// Удалает все новые настройки, которые не были применены
    /// </summary>
    public void CancelSetting()
    {
        _newSetting = new Dictionary<TypeSetting, object>();
        _isUpdate = false;
        Debug.Log($"Временные настройки отменены");
    }
    /// <summary>
    /// Добавляет в новые настройки базовые значениия, базовые значения беруться из файла.
    /// </summary>
    public void ResetSetting()
    {
        if (DefaultSettingData == null)
        {
            Debug.LogWarning($"DefaultSetting == null");
            return;
        }
        RegisterSetting(TypeSetting.fullscreen, DefaultSettingData.DefaultSetting.FullScreen);
        RegisterSetting(TypeSetting.music, DefaultSettingData.DefaultSetting.Music);
        RegisterSetting(TypeSetting.sound, DefaultSettingData.DefaultSetting.Sound);
        RegisterSetting(TypeSetting.resolution, $"{DefaultSettingData.DefaultSetting.Resolution.Width}" +
            $":{DefaultSettingData.DefaultSetting.Resolution.Height}");
        IsDefaultSetting = true;
        Debug.Log($"Базовые настройки применены");
    }
    /// <summary>
    /// Сохраняет и принимает все новые настройки, также очищает обощенную колекцию
    /// </summary>
    public void UpdateSetting()
    {
        foreach(var setting in _newSetting)
        {
            SavesAllNewSettings(setting.Key, setting.Value);
        }
        ApplySetting();
        _newSetting = new Dictionary<TypeSetting, object>();
        Debug.Log($"Временные настройки применены");
    }
    /// <summary>
    /// Сохраняет все новые настройки
    /// </summary>
    /// <param name="key">Ключ настройки</param>
    /// <param name="value">Значение настройки</param>
    private void SavesAllNewSettings(TypeSetting key, object value)
    {
        if (value == null) return;
        SaveNewResolution(key, value);
        SaveNewSound(key, value);
        SaveNewMusic(key, value);
        SaveNewFullscreen(key, value);
        SaveNewLanguage(key, value);
    }
    /// <summary>
    /// Проверяет являются новые настройки актуальными
    /// </summary>
    /// <param name="key">Ключ настройки</param>
    /// <param name="value">Значение настройки</param>
    private bool IsNewSetting(TypeSetting key, object value)
    {
        if (value == null) return false;
        return IsNewResolution(key, value) ||
            IsNewSound(key, value) ||
            IsNewMusic(key, value) ||
            IsNewFullscreen(key, value) ||
            IsNewLanguage(key, value);
    }
    /// <summary>
    /// Шаблоновый метод для проверки актульности настроек
    /// </summary>
    /// <typeparam name="T">Тип переменной</typeparam>
    /// <param name="key">Ключ настройки</param>
    /// <param name="value">Значение настройки</param>
    /// <param name="loadFunc">Выполняемая функция с возратом определенного значения</param>
    /// <param name="expectedKey">Проверяемый ключ</param>
    /// <returns></returns>
    private bool IsNewSettingInternal<T>(TypeSetting key, object value, Func<T> loadFunc,TypeSetting expectedKey)
    {
        if (key != expectedKey) return false;
        if (value is T typedValue)
        {
            return !Equals(typedValue, loadFunc());
        }
        return false;
    }
    /// <summary>
    /// Шаблоновый метод для применение настроек
    /// </summary>
    /// <typeparam name="T">Тип переменной</typeparam>
    /// <param name="key">Ключ настройки</param>
    /// <param name="value">Значение настройки</param>
    /// <param name="saveAction">Выполняемая функция с возратом определенного значения</param>
    /// <param name="expectedKey">Проверяемый ключ</param>
    private void SaveNewSettingInternal<T>(TypeSetting key, object value, Action<T> saveAction, TypeSetting expectedKey)
    {
        if (key != expectedKey) return;
        if (value is T typedValue)
        {
            saveAction(typedValue);
        }
    }
    private bool IsNewLanguage(TypeSetting key, object value) =>
        IsNewSettingInternal<string>(key, value, () => SettingStorage.LoadLanguage(), TypeSetting.language);
    private void SaveNewLanguage(TypeSetting key, object value) =>
        SaveNewSettingInternal<string>(key, value, lang => SettingStorage.SaveLanguage(lang), TypeSetting.fullscreen);
    private bool IsNewFullscreen(TypeSetting key, object value) =>
        IsNewSettingInternal<bool>(key, value, () => SettingStorage.LoadFullscreen(), TypeSetting.fullscreen);
    private void SaveNewFullscreen(TypeSetting key, object value) =>
        SaveNewSettingInternal<bool>(key, value, lang => SettingStorage.SaveFullscreen(lang), TypeSetting.fullscreen);
    private bool IsNewMusic(TypeSetting key, object value) =>
        IsNewSettingInternal<bool>(key, value, () => SettingStorage.LoadMusic(), TypeSetting.music);
    private void SaveNewMusic(TypeSetting key, object value) =>
        SaveNewSettingInternal<bool>(key, value, lang => SettingStorage.SaveMusic(lang), TypeSetting.music);
    private bool IsNewSound(TypeSetting key, object value) =>
        IsNewSettingInternal<bool>(key, value, () => SettingStorage.LoadSound(), TypeSetting.sound);
    private void SaveNewSound(TypeSetting key, object value) =>
        SaveNewSettingInternal<bool>(key, value, lang => SettingStorage.SaveSound(lang), TypeSetting.sound);
    private bool IsNewResolution(TypeSetting key, object value) =>
        IsNewSettingInternal<ResolutionData>(key, value, () => SettingStorage.LoadResolution(), TypeSetting.resolution);
    private void SaveNewResolution(TypeSetting key, object value) =>
        SaveNewSettingInternal<ResolutionData>(key, value, lang => SettingStorage.SaveResolution(lang), TypeSetting.resolution);

    /// <summary>
    /// Применение всех сохраненных настроек
    /// </summary>
    private void ApplySetting()
    {
        ResolutionData resolution = SettingStorage.LoadResolution();
        bool fullscreen = SettingStorage.LoadFullscreen();
        Screen.SetResolution(resolution.Width, resolution.Height, fullscreen);
        InvokeManager.Instance.InvokeForKey("music");
        InvokeManager.Instance.InvokeForKey("sound");
    }
    private void UnregisterNewSetting(TypeSetting key)
    {
        _newSetting.Remove(key);
    }
}
