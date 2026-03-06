using Assets.Scripts.Core.Interface;
using UnityEngine;

public abstract class BaseManager : MonoBehaviour
{
    protected bool IsInitialize = false;
    public virtual string Register(string key, object obj)
    {
        return "NotFoundKey";
    }
    public virtual void Unregister(string key) 
    {
        Debug.LogWarning("Вы забыли переиспользовать этот метод");
    }

    protected void UpdateInitialize()
    {
        IsInitialize = !IsInitialize;
    }
}
