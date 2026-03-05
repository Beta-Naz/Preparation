using Assets.Scripts.Core.Array;
using Assets.Scripts.Core.Interface;
using Assets.Scripts.Data.SettingsDatas;
using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    [CreateAssetMenu(fileName = "SettingDatas", menuName = "Game/SettingDatas")]
    public class SettingDatas : ScriptableObject, ILoadFromJson
    {
        public SettingData DefaultSetting;
        private readonly string _path = "Data/SettingData"; //Вместо SettingData был LevelDatas долго понять ошибку не мог
        public void LoadFromJson()
        {
            try
            {
                TextAsset jsonFile = Resources.Load<TextAsset>(_path);
                if(jsonFile == null)
                {
                    Debug.LogError("jsonFile == null");
                    return;
                }
                string jsonText = jsonFile.text;
                SettingDataContainer json = JsonUtility.FromJson<SettingDataContainer>(jsonText);
                if (json != null)
                {
                    DefaultSetting = json.ArrayOfSettingData.DefaultSetting;
                    Debug.Log("Базовые настройки успешно загружены", this);
                }
                else
                {
                    Debug.LogError("json == null", this);
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
            }
        }
    }
}
