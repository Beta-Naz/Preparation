using Assets.Scripts.Core.Interface;
using UnityEngine;

public abstract class BaseManager : MonoBehaviour
{
    protected bool IsInitialize = false;
    public virtual string Register(string key, object obj)
    {
        return "NotFoundKey";
    }
    public abstract void Unregister(string key);

    protected void UpdateInitialize()
    {
        IsInitialize = !IsInitialize;
    }
}
