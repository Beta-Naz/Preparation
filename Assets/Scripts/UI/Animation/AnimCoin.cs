using UnityEngine;

public class AnimCoin : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _distanceDeestroy = 40f;
    [SerializeField] private float _timeAnimation = 0.3f;
    private int _addCoins = 0;
    public int addCoins
    {
        get
        {
            return _addCoins;
        }
        set
        {
            if (value > 0)
            {
                _addCoins = value;
            }
            else
            {
                _addCoins = 0;
            }
        }
    }
    private Transform _startPosition;
    private Vector3 _endPosition;
    private float _startTime;
    private float _distance;
    void Start()
    {
        _startPosition = transform;
        _startTime = Time.time;
        _endPosition = new Vector3(1751.287f, 1027f, 0f);
        _distance = Vector3.Distance(_startPosition.position, _endPosition);
    }
    void FixedUpdate()
    {
        if(_endPosition == null)
        {
            return;
        }
        if(_timeAnimation > 0)
        {
            _timeAnimation -= Time.deltaTime;
            return;
        }
        float speed = (Time.time - _startTime) * _speed;
        float currentSpeed = speed / _distance;
        transform.position = Vector3.Lerp(_startPosition.position, _endPosition, currentSpeed);
        float distanceDestroy = Vector3.Distance(transform.position, _endPosition);
        if (distanceDestroy < _distanceDeestroy)
        {
            int allCoin = PlayerPrefs.GetInt("Money", 0);
            PlayerPrefs.SetInt("Money", allCoin + addCoins);
            InvokeManager.Instance.InvokeForKey("coin");
            Destroy(gameObject);
        }
    }
}
