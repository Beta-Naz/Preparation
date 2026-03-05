using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class ObjectData
    {
        public Vector3Data Position;
        public Vector3Data Rotation;
        public Vector3Data Scale;
        public int ObjectType;
    }
}