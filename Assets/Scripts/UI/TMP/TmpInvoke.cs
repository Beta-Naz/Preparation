using UnityEngine;
using Assets.Scripts.Core.Interface;
using TMPro;

namespace Assets.Scripts.UI.TMP
{
    public class TmpInvoken : MonoBehaviour, IInvoke
    {
        [SerializeField] private string _invokeKey = "coin";
        private TextMeshProUGUI _tmp;
        public void Start()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
            InvokeManager.Instance.Register(_invokeKey, this);
            Invoke();
        }
        public void Invoke()
        {
            float allCoin = PlayerPrefs.GetInt("Money", 0);
            if(allCoin >= 1000)
            {
                allCoin /= 1000;
                _tmp.text = $"{Mathf.Round(allCoin * 100) / 100}k";
            }
            else
            {
                _tmp.text = $"{allCoin}";
            }
        }
    }
}