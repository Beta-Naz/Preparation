using Assets.Scripts.Core.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvokeManager : BaseManager, IInitializable
{
    public static InvokeManager Instance { get; private set; }
    private Dictionary<string, IInvoke> _keyValueInvoke;
    private bool _isStart;
    public bool IsStart
    {
        get => _isStart;
        set
        {
            _isStart = value;
            InvokeForKey("game");
        }
    }
    public override string Register(string key, object obj)
    {
        if(_keyValueInvoke == null)
        {
            Debug.LogWarning("_keyValueInvoke == null", this);
            return null;
        }
        if(obj is IInvoke invoke)
        {
            string newKey = key;
            if (_keyValueInvoke.ContainsKey(newKey))
            {
                int index = 0;
                do
                {
                    newKey = $"{key}_{index}";
                    index++;
                }
                while (_keyValueInvoke.ContainsKey(newKey));
            }
            _keyValueInvoke[newKey] = invoke;
            _keyValueInvoke[newKey].Invoke();
            Debug.Log($"{key} успешно зарегистрирован");
        }
        else
        {
            Debug.LogWarning("Ќе удалось преобразовать obj в IInvoke", this);
        }
        return null;
    }
    public override void Unregister(string key)
    {
        if (_keyValueInvoke == null)
        {
            return;
        }
        else if (Instance == null || this == null)
        {
            return;
        }
        if (_keyValueInvoke.ContainsKey(key))
        {
            _keyValueInvoke.Remove(key);
        }
    }

    void IInitializable.Initialize()
    {
       if(Instance != null && Instance != this)
       {
            Destroy(gameObject);
            return;
       }
       Instance = this;
       transform.SetParent(null);
       DontDestroyOnLoad(gameObject);
       _keyValueInvoke = new Dictionary<string, IInvoke>();
       Debug.Log($"менеджер InvokeManager успешно инициализирован");
    }
    public void InvokeForKey(string key)
    {
        if(_keyValueInvoke == null)
        {
            Debug.LogWarning("_keyValueInvoke == null", this);
            return; 
        }
        foreach(var obj in _keyValueInvoke)
        {
            if (obj.Key.Contains(key))
            {
                obj.Value.Invoke();
            }
        }
    }
    public void ClearMassiv()
    {
        _keyValueInvoke = new Dictionary<string, IInvoke>();
    }
}
