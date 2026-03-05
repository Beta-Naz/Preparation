using Assets.Scripts.Core.Array;
using Assets.Scripts.Core.Interface;
using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

namespace Assets.Scripts.Core
{
	[CreateAssetMenu (fileName = "DatasLevel", menuName = "Game/DatasLevel")]
	public class DatasLevel : ScriptableObject, ILoadFromJson
    {
		public List<LevelData> Levels = new List<LevelData>();
		private readonly string _path = UnityEngine.Application.streamingAssetsPath + "/LevelDatas.json";
		public void LoadFromJson()
		{
			try
			{
                if (!File.Exists(_path))
                {
                    Debug.LogError($"Не найден файл по ссылке {_path}");
                    return;
                }
                string jsonText = File.ReadAllText(_path);
                LevelDataContainer json = JsonUtility.FromJson<LevelDataContainer>(jsonText);
                if (json != null)
                {
                    Levels = json.ArrayOfLevelData.LevelData;
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