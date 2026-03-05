using Assets.Scripts.Core.Base;
using UnityEngine;

public class ImageAddLink : BaseAddLink
{
    protected override void DebugMessage()
    {
        Debug.Log($"Панель зарегистрована под ключом: {Key}", this);
        if (ObjectManager.KeyValueGameObject.ContainsKey(Key))
        {
            if (ObjectManager.KeyValueGameObject[Key])
            {
                Debug.Log($"{ObjectManager.KeyValueGameObject[Key].transform.position.x} {ObjectManager.KeyValueGameObject[Key].transform.position.y}", this);
            }
        }
    }
}
