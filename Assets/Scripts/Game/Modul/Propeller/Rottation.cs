using UnityEngine;
using System.Collections;
using Assets.Scripts.Core.Base;

namespace Assets.Scripts.Game.Modul.Propeller
{
	public class Rottation : BaseModuls
    {
        [SerializeField] private float _speedRotation = 180;
        public override void Invoke()
        {
            IsStart = InvokeManager.IsStart;
            
        }
        private void Update()
        {
            if (IsStart)
            {
                transform.eulerAngles = new Vector3(0, 0, _speedRotation * Time.deltaTime);
            }
        }
    }
}