using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Core.Interface.Game
{
    public interface IObjectFactory
    {
        GameObject CreateObject(ObjectData data);
        GameObject CreateCar(CarData data);
        GameObject CreateFinish(Vector3 position);
    }
}
