using UnityEngine;
using System;
[Serializable]
public class Vector3Data
{
    public float X, Y, Z;
    public Vector3 ToVector() => new(X, Y, Z);
}
