using Assets.Scripts.Core.Interface;
using UnityEngine;

public class SoundVolume : MonoBehaviour, IInvoke
{
    AudioSource _audioSource;
    public void Invoke()
    {
        if (_audioSource == null) return;
        Debug.Log("ћне пришло уведомление дл€ обновлени€ музыки");
        _audioSource.mute = PlayerPrefs.GetInt("Sound") == 0;
    }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        InvokeManager.Instance.Register("sound", this);
    }
}
