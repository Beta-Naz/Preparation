using Assets.Scripts.Core.Interface;
using Assets.Scripts.Core.Interface.Game;
using Assets.Scripts.Data;
using Assets.Scripts.Game.Level;
using UnityEngine;

namespace Assets.Scripts.Core.Manager
{
	public class LevelManager: MonoBehaviour, IInitializable
	{
		public static LevelManager Instance { get; private set; } 
        public GameObject CurrentCar { get; private set; }
        [SerializeField] private DatasLevel _datasLevel;
        [SerializeField] private GameObject _gameWinPanel;
        [SerializeField] private GameObject _gameLosePanel;
        [SerializeField] private GameObject[] _objectActive;
        [SerializeField] private GameConfiguration _configuration;
        private ILevelDataProvider _levelDataProvider;
        private IObjectFactory _objectFactory;
        private ILevelUIManager _uiLevelManager;
        public ILevelUIManager UiLevelManager => _uiLevelManager;
        private void Awake()
		{
			Initialize();
            int levelIndex = PlayerPrefs.GetInt("CurrentLevel", 0); //Костыль, пока оставлю как есть
            CreateLevel(levelIndex);
        }
        /// <summary>
        /// Стандартный метод инциализации для менеджера, инициализируется в BaseManager
        /// </summary>
        public void Initialize()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            _levelDataProvider = new LevelDataProvider(_datasLevel);
            _objectFactory = new GameObjectFactory(_configuration);
            _uiLevelManager = new LevelUIManager(_gameWinPanel, _gameLosePanel, _objectActive);
            Debug.Log($"LevelManager успешно инициализирован");
        }
        /// <summary>
        /// Создание уровня
        /// </summary>
        /// <param name="levelIndex">номер уровня</param>
        public void CreateLevel(int levelIndex)
        {
            try
            {
                if (!_levelDataProvider.HasLevel(levelIndex))
                {
                    levelIndex = 0;
                    if (!_levelDataProvider.HasLevel(levelIndex))
                    {
                        Debug.LogError("Нет доступных уровней");
                        return;
                    }
                }
                var levelData = _levelDataProvider.GetLevel(levelIndex);
                var lastObject = CreateObjectsFromData(levelData.Objects);
                if (lastObject != null)
                {
                    var finishPosition = lastObject.transform.position + new Vector3(0, 0.3f, 0);
                    _objectFactory.CreateFinish(finishPosition);
                }
                CurrentCar = _objectFactory.CreateCar(levelData.CarData);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Ошибка при создании уровня: {e.Message}");
            }
        }
        /// <summary>
        /// Спавн для объектов
        /// </summary>
        /// <param name="objectsData">Все объекты, которые есть на LevelData</param>
        /// <returns></returns>
        private GameObject CreateObjectsFromData(Objects objectsData)
        {
            if (objectsData?.ObjectData == null)
                return null;

            GameObject lastObject = null;
            float maxX = float.MinValue;

            foreach (var objData in objectsData.ObjectData)
            {
                if (objData == null)
                    continue;

                var newObject = _objectFactory.CreateObject(objData);
                if (newObject != null)
                {
                    // Логика определения последнего объекта
                    if (newObject.transform.position.x > maxX &&
                        newObject.transform.position.z < 4)
                    {
                        maxX = newObject.transform.position.x;
                        lastObject = newObject;
                    }
                }
            }
            return lastObject;
        }
    }
}