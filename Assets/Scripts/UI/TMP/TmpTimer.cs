using Assets.Scripts.Core.Base;
using TMPro;
using UnityEngine;

public class TmpTimer : BaseAddLink
{
    [SerializeField] private float _defaultTime = 10f;
    private float _currentTime = 0f;
    private bool _isStart = false;
    private TextMeshProUGUI _tmp;
    private void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        UpdateTimer();
    }
    private void Update()
    {
        if (!_isStart || _currentTime <= 0f)
        {
            return;
        }
        _currentTime -= Time.deltaTime;
        if(_currentTime > 0f)
        {
            UpdateText();
        }
        else
        {
            _isStart = false;
            _tmp.text = "0";
            EndTimer();
        }
    }
    public void UpdateTimer()
    {
        _currentTime = _defaultTime;
        _isStart = true;
    }
    private void UpdateText()
    {
        if( _tmp != null)
        {
            _tmp.text = Mathf.Round(_currentTime).ToString();
        }
        else
        {
            Debug.LogWarning(this);
        }
    }
    private void EndTimer()
    {
        if(ObjectManager.Instance != null)
        {
            ObjectManager.Instance.KeyValueGameObject["PanelSaveSettings"].SetActive(false);
            ObjectManager.Instance.KeyValueGameObject["PanelSetting"].SetActive(true);
            ObjectManager.Instance.KeyValueGameObject["PanelBLockForSetting"].SetActive(false);
            if (SettingManager.Instance != null)
            {
                SettingManager.Instance.CancelSetting();
            }
        }
      
    }
    protected override void DebugMessage()
    {
        Debug.Log($"Текст зарегистрован под ключом: {Key}", this);
    }
}
