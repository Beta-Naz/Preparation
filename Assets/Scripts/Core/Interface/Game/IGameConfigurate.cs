using UnityEngine;

namespace Assets.Scripts.Core.Interface.Game
{
    public interface IGameConfigurate
    {
        GameObject[] GetObjectsPrefab();
        GameObject[] GetCarPrefabs();
        GameObject GetFinishPrefab();
    }
}
