using Assets.Scripts.Core.Interface;
using Assets.Scripts.Core.Manager;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : BaseManager, IInitializable
{

    public static ObjectManager Instance { get; private set; }
    [Header("PrefabAnimCoin")]
    [SerializeField] private GameObject _animCoin;
    public GameObject AnimCoin => _animCoin;

    private Dictionary<string, GameObject> _keyValueGameObject;
    public Dictionary<string, GameObject> KeyValueGameObject => _keyValueGameObject;

    public AudioClip AudioClip;
    public void Initialize()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        _keyValueGameObject = new Dictionary<string, GameObject>();    
        Debug.Log($"менеджер ObjectManager успешно инициализирован");
    }
    public override string Register(string key, object obj)
    {
        if (_keyValueGameObject == null)
        {
            return null;
        }
        if (obj != null && obj is GameObject invoke)
        {
            string newKey = key;
            //if (_keyValueGameObject.ContainsKey(newKey))
            //{
            //    int index = 0;
            //    do
            //    {
            //        newKey = $"{key}_{index}";
            //        index++;
            //    }
            //    while (_keyValueGameObject.ContainsKey(newKey));
            //}
            _keyValueGameObject[newKey] = invoke;
            Debug.Log($"{newKey} успешно зарегистрирован");
            return newKey;
        }
        else
        {
            Debug.LogWarning($"Не удалось преобразовать obj в GameObject ключ {key}", this);
        }
        return null;
    }

    public override void Unregister(string key)
    {
        if (_keyValueGameObject == null)
        {
            return;
        }
        if (_keyValueGameObject.ContainsKey(key))
        {
            _keyValueGameObject.Remove(key);
        }
    }
}
