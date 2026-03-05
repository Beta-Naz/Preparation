using Assets.Scripts.Core.Base;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Button
{
    public class BtnBlockThis : BaseButton
    {
        [SerializeField] private float _defaultTime = 30f;
        private float _currentTime = 0f;
        private bool _isStart = false;
        private void Update()
        {
            if (!_isStart)
            {
                return;
            }
            _currentTime -= Time.deltaTime;
            if (_currentTime < 0f)
            {
                _isStart = false;
                ThisButton.interactable = true;
            }
        }
        public void UpdateTimer()
        {
            Debug.Log("TimerUpdate");
            _currentTime = _defaultTime;
            _isStart = true;
            ThisButton.interactable = false;
        }
        protected override void OnClick()
        {
            UpdateTimer();
        }
    }
}
