using Assets.Scripts.Core.Interface;
using UnityEngine;

public class BackgroundVolume : MonoBehaviour, IInvoke
{
    AudioSource _audioSource;
    public void Invoke()
    {
        if (_audioSource == null) return;
        Debug.Log("Мне пришло уведомление для обновления музыки");
        _audioSource.mute = PlayerPrefs.GetInt("Music") == 0;
    }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        InvokeManager.Instance.Register("music", this); //Ошибка из-за который я потерял балл, написал Music вместо music 
    }
}
