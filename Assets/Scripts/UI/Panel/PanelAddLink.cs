using Assets.Scripts.Core.Base;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanelAddLink : BaseAddLink
{
    protected override void DebugMessage()
    {
        Debug.Log($"Панель зарегистрована под ключом: {Key}", this);
    }
}
