using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.TMP
{
    public class TmpTImerForStoreCoin : MonoBehaviour
	{
        [SerializeField] private float _defaultTime = 3600f;
        [SerializeField] private UnityEngine.UI.Button _button;
        [SerializeField] private TextMeshProUGUI _tmp;
        private float _currentTime = 0f;
        private bool _isStart = false;
        private void Start()
        {
            UpdateTimer();
        }
        private void Update()
        {
            if (!_isStart)
            {
                return;
            }
            _currentTime -= Time.deltaTime;
            if (_currentTime > 0f)
            {
                UpdateText();
            }
            else
            {
                _isStart = false;
                _button.interactable = true;
                PlayerPrefs.SetFloat("Time", 3600);
                if (_tmp != null)
                {
                    _tmp.text = "00:00";
                }
            }
        }
        private void ConvertToTime()
        {
            string minuts = $"{(int)_currentTime / 60}" ;
            string seconds = $"{(int)_currentTime % 60}";
            minuts = minuts.Length == 1 ? "0" + minuts : minuts;
            seconds = seconds.Length == 1 ? seconds + "0" : seconds;
            _tmp.text = $"{minuts}:{seconds}";
        }
        public void UpdateTimer()
        {
            try
            {
                Debug.Log("TimerUpdate");
                _currentTime = _defaultTime;
                _isStart = true;
                _button.interactable = false;
                UpdateText();
            }
            catch
            {

            }
        }
        private void UpdateText()
        {
            if (_tmp != null)
            {
                ConvertToTime();
            }
        }
        private void OnDestroy()
        {
            PlayerPrefs.SetFloat("Time", _currentTime);
        }
    }
}