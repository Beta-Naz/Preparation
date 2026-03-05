using UnityEngine;
using System.Collections;
using Assets.Scripts.Core.Interface;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core.Manager
{
	public class InitializedManager : MonoBehaviour
	{
        public static InitializedManager Instance {  get; private set; }
        [SerializeField] private MonoBehaviour[] AllManagers;
        void Awake()
        {
            Initialize();
            IntializeManagers();
            LoadFirstScene();
        }
        void Initialize()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        void IntializeManagers()
        {
            if(AllManagers == null)
            {
                return;
            }
            for (int i = 0; i < AllManagers.Length; i++)
            {
                if(AllManagers[i] == null)
                {
                    Debug.LogWarning($"id {i} равняется null", this);
                    continue;
                }
                if(AllManagers[i] is IInitializable initializable)
                {
                    initializable.Initialize();
                }
            }
        }
        void LoadFirstScene()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}