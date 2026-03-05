using UnityEngine;
using System.Collections;
using Assets.Scripts.Core.Base;

namespace Assets.Scripts.Game.Modul.Wheel
{
	public class Rottation : BaseModuls
	{
        [SerializeField] private float _speedRotation;
        public override void Invoke()
        {
            IsStart = InvokeManager.IsStart;
            Debug.Log($"Колеса уведомлены {IsStart}");
        }
        private void Update()
        {
            if (IsStart)
            {
                transform.eulerAngles = new Vector3(0,0, _speedRotation * Time.deltaTime);
            }
        }
    }
}