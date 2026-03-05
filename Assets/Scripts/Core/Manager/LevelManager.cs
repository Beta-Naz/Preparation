using Assets.Scripts.Core.Interface;
using Assets.Scripts.Data;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Manager
{
	public class LevelManager: MonoBehaviour, IInitializable
	{
		public static LevelManager Instance { get; private set; }
		[SerializeField] private DatasLevel _datasLevel;
		[SerializeField] private GameObject[] _allObjects;
		[SerializeField] private GameObject[] _allTypesCar;
        [SerializeField] private GameObject _prefabFinish;

        private List<GameObject> _currentSpawnObject = new();
		public GameObject CurrentCar;

        [SerializeField] private GameObject _gameWinPanel;
        public GameObject GameWinPanel => _gameWinPanel;

        [SerializeField] private GameObject _gameLosePanel;
        public GameObject GameLosePanel => _gameLosePanel;


        [SerializeField] private GameObject[] _objectActive;
        public GameObject[] ObjectActive => _objectActive;
        private void Awake()
		{
			Initialize();
            int levelIndex = PlayerPrefs.GetInt("CurrentLevel", 0);
            CreateLevel(levelIndex);
        }
        public void Initialize()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            Debug.Log($"LevelManager успешно инициализирован");
        }
        public void CreateLevel(int levelIndex)
        {
            if(_datasLevel == null)
            {
                Debug.LogError("_datasLevel == null", this);
                return;
            }
            _datasLevel.LoadFromJson();
            if (_datasLevel.Levels == null && _datasLevel.Levels.Count > 0)
            {
                Debug.LogError("_datasLevel.Levels == null", this);
                return;
            }
            if(_datasLevel.Levels.Count < levelIndex)
            {
                levelIndex = 0;
            }
            if (_allObjects == null && _allObjects.Length == 0)
            {
                Debug.LogError("_allObjects == null", this);
                return;
            }
            if (_allTypesCar == null && _allTypesCar.Length == 0)
            {
                Debug.LogError("_allTypesCar == null", this);
                return;
            }
            CreateObjects(levelIndex);
            CreateCar(_datasLevel.Levels[levelIndex].CarData);
        }
        private void CreateObjects(int levelIndex)
        {
            Debug.Log("Начанием создавать объекты", this);
            Objects objects = _datasLevel.Levels[levelIndex].Objects;
            float maxX = float.MinValue;
            GameObject lastObject = null;
            foreach (var obj in objects.ObjectData)
            {
                if(obj != null)
                {
                    if(_allObjects.Length > obj.ObjectType)
                    {
                        GameObject prefab = _allObjects[obj.ObjectType];
                        if(prefab != null)
                        {
                            GameObject newObject = Instantiate(prefab);
                            newObject.transform.position = obj.Position.ToVector();
                            newObject.transform.eulerAngles = obj.Rotation.ToVector();
                            newObject.transform.localScale = obj.Scale.ToVector();
                            _currentSpawnObject.Add(newObject);
                            if(newObject.transform.position.x > maxX && newObject.transform.position.z < 4)
                            {
                                maxX = newObject.transform.position.x;
                                lastObject = newObject;
                            }
                        }
                        else
                        {
                            Debug.LogWarning("prefab == null", this);
                            continue;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("_allObjects.Length < obj.ObjectType", this);
                        continue;
                    }
                }
                else
                {
                    Debug.LogWarning("obj == null", this);
                    continue;
                }
            }
            CreateFinish(lastObject);
        }
        private void CreateFinish(GameObject obj)
        {
            if (obj == null)
            {
                Debug.LogError("obj == null", this);
                return;
            }
            if(_prefabFinish != null)
            {
                GameObject finishObject = Instantiate(_prefabFinish, obj.transform);
                finishObject.transform.localPosition = new Vector3(0, 0.3f, 0);
            }
            else
            {
                Debug.LogError("_prefabFinish == null", this);
                return;
            }
        }
        private void CreateCar(CarData car)
        {
            if(car == null)
            {
                Debug.LogError("car == null", this);
                return;
            }
            if(_allTypesCar.Length < car.CarModelIndex)
            {
                Debug.LogError("_allTypesCar.Length < car.CarModelIndex", this);
                return;
            }
            GameObject prefabCar = _allTypesCar[car.CarModelIndex];
            if(prefabCar == null)
            {
                Debug.LogError("prefabCar == null", this);
                return;
            }
            GameObject carObject = Instantiate(prefabCar);
            Rigidbody2D rigid = carObject.AddComponent<Rigidbody2D>();
            rigid.gravityScale = 10;
            carObject.transform.position = new Vector3(0, 0.5f, 0);
            carObject.AddComponent<Move>();
            CurrentCar = carObject;
        }
    }
}