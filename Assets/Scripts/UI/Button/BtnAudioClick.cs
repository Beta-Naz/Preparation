using Assets.Scripts.Core.Interface;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.UI.Button
{
	public class BtnAudioClick: MonoBehaviour, IInvoke
	{
		private AudioClip clip;
		private AudioSource audioSource;
		private UnityEngine.UI.Button button;
		void Start()
		{
			if(gameObject.TryGetComponent<AudioSource>(out _))
			{
                audioSource = GetComponent<AudioSource>();
			}
			button = GetComponent<UnityEngine.UI.Button>();
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            audioSource.clip = clip;

            button.onClick.AddListener(OnClick);
			InvokeManager.Instance.Register("sounds", this);
        }
		private void OnClick()
		{
            audioSource.Play();
        }
        public void Invoke()
        {
			audioSource.mute = PlayerPrefs.GetInt("Sounds") != 0;
        }
    }
}