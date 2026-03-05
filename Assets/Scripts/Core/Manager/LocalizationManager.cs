using UnityEngine;
using System.Collections;
using Assets.Scripts.Core.Interface;

namespace Assets.Scripts.Core.Manager
{
	public class LocalizationManager : MonoBehaviour, IInitializable
	{
        public static LocalizationManager Instance {  get; private set; }
        [SerializeField] private string _defaultLanguage = "en";
        private string _currentLanguage = "en";
		public void ChooseLanguage()
		{
            _currentLanguage = PlayerPrefs.GetString("language", _defaultLanguage);
        }
        public void Initialize()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            Debug.Log($"менеджер LocalizationManager успешно инициализирован");
        }
    }
}