using Assets.Scripts.Core.Interface.Game;
using UnityEngine;

namespace Assets.Scripts.Core
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "Game/GameConfiguration")]
    public class GameConfiguration : ScriptableObject, IGameConfigurate
    {
        [SerializeField] private GameObject[] _allObjects;
        [SerializeField] private GameObject[] _allTypesCar;
        [SerializeField] private GameObject _prefabFinish;
        public GameObject[] GetCarPrefabs() => _allTypesCar;
        public GameObject[] GetObjectsPrefab() => _allObjects;
        public GameObject GetFinishPrefab() => _prefabFinish;
    }
}
