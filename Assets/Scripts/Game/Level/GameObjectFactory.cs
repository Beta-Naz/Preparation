using Assets.Scripts.Core.Interface.Game;
using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Level
{
    public class GameObjectFactory : IObjectFactory
    {
        private readonly IGameConfigurate _configuration;
        private readonly List<GameObject> _spawnedObjects;
        public IReadOnlyList<GameObject> SpawnedObjects => _spawnedObjects;
        public GameObjectFactory(IGameConfigurate configuration)
        {
            _configuration = configuration;
            _spawnedObjects = new List<GameObject>();
        }
        public GameObject CreateCar(CarData data)
        {
            var carPrefabs = _configuration.GetCarPrefabs();
            if (carPrefabs == null || carPrefabs.Length <= data.CarModelIndex)
                return null;

            var prefab = carPrefabs[data.CarModelIndex];
            if (prefab == null)
                return null;

            var car = GameObject.Instantiate(prefab);
            if(car == null) 
                return null;
            SetupCarComponents(car);
            return car;
        }

        public GameObject CreateFinish(Vector3 position)
        {
            var finishPrefab = _configuration.GetFinishPrefab();
            if (finishPrefab == null)
                return null;

            var finish = GameObject.Instantiate(finishPrefab);
            finish.transform.position = position;
            return finish;
        }

        public GameObject CreateObject(ObjectData data)
        {
            var prefabs = _configuration.GetObjectsPrefab();
            if (prefabs == null || prefabs.Length <= data.ObjectType)
            {
                Debug.LogWarning("prefabs == null || prefabs.Length <= data.ObjectType");
                return null;
            }
            var prefab = prefabs[data.ObjectType];
            if (prefab == null)
            {
                Debug.LogWarning("prefab == null");
                return null;
            }
            var newObject = GameObject.Instantiate(prefab);
            newObject.transform.position = data.Position.ToVector();
            newObject.transform.eulerAngles = data.Rotation.ToVector();
            newObject.transform.localScale = data.Scale.ToVector();
            _spawnedObjects.Add(newObject);
            return newObject;
        }
        private void SetupCarComponents(GameObject car)
        {
            var rigidbody = car.GetComponent<Rigidbody2D>();
            if (rigidbody == null)
            {
                rigidbody = car.AddComponent<Rigidbody2D>();
                rigidbody.gravityScale = 10;
            }

            if (car.GetComponent<Move>() == null)
                car.AddComponent<Move>();
        }

    }
}
