using UnityEngine;
using System.Collections;
using Assets.Scripts.Core.Manager;
using Assets.Scripts.Core.Interface;
using Assets.Scripts.Core.Base;

namespace Assets.Scripts.Game
{
	public class CinemaCamera : MonoBehaviour
	{
		private Transform _player;
		private Vector3 velocity;
		private float _defaultY;
        private float _defaultZ;
        void Start()
		{
			ResetPlayer();
        }
        private void ResetPlayer()
        {
			GameObject player = LevelManager.Instance.CurrentCar;
			_player = player.transform;
            _defaultY = transform.position.y - _player.position.y;
			Debug.Log($"Player is find {_player.position.x},{_player.position.y},{_player.position.z}");
            Debug.Log($"Player is find {player.name}");
        }
        void Update()
		{
			if( _player != null)
			{
				Vector3 current = _player.position;
                current.y += _defaultY;
				current.z = transform.position.z;
                transform.position = Vector3.SmoothDamp(
					transform.position,
                    current, 
					ref velocity, 
					0.3f);
			}
			else
			{
				Debug.LogWarning("PlayerNull");
				ResetPlayer();
            }
		}
    }
}