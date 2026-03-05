using Assets.Scripts.Core.Base;
using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

namespace Assets.Scripts.UI.Button
{
	public class BtnSpawnAnimation: BaseButton
	{
        [SerializeField] private string _keyObjectSpawn = "ImageSpawnCoin";
        [SerializeField] private int _addCoins = 100;
        protected override void OnClick()
        {
            if(ObjectManager.Instance == null)
            {
                return; 
            }
            GameObject prefab = ObjectManager.Instance.AnimCoin;
            GameObject animCoin = Instantiate(prefab, ObjectManager.Instance.KeyValueGameObject[_keyObjectSpawn].transform);
            animCoin.transform.localPosition = Vector3.zero;
            AnimCoin animCoin1 = animCoin.GetComponent<AnimCoin>();
            animCoin1.addCoins = _addCoins;
        }
	}
}