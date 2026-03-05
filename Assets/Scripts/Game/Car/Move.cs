using Assets.Scripts.Core.Base;
using UnityEngine;

public class Move : BaseModuls
{
    [SerializeField] private float _speed = 5;
    private Rigidbody2D _rigidbody2;
    protected override void Start()
    {
        base.Start();
        _rigidbody2 = GetComponent<Rigidbody2D>();
        Invoke();
    }
    public override void Invoke()
    {
        IsStart = InvokeManager.IsStart;
    }
    private void Update()
    {
        if (IsStart)
        {
            Vector3 movement = Vector3.zero;
            movement.x += 1;
            if(_rigidbody2 != null)
            {
                _rigidbody2.linearVelocity = movement * _speed;
            }
        }
    }
}
