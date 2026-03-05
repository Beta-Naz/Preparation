using UnityEngine;
using System.Collections;
using Assets.Scripts.Core.Interface;

namespace Assets.Scripts.Core.Manager
{
    public class SpriteManager : MonoBehaviour, IInitializable
    {
        public static SpriteManager Instance {  get; private set; }
        [Header("Link to sprite tgl_off")]
        [SerializeField] private Sprite _spriteTglOff;
        public Sprite SpriteTglOff => _spriteTglOff;
        [Header("Link to sprite tgl_On")]
        [SerializeField] private Sprite _spriteTglOn;
        public Sprite SpriteTglOn => _spriteTglOn;
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
            Debug.Log($"менеджер SpriteManager успешно инициализирован");
        }
    }
}