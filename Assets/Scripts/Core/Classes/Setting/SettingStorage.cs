using Assets.Scripts.Data.SettingsDatas;
using UnityEngine;

namespace Assets.Scripts.Core.Classes.Setting
{
    public class SettingStorage
    {
        public void SaveResolution(ResolutionData resolutionData)
        { 
            PlayerPrefs.SetInt("Width", resolutionData.Width);
            PlayerPrefs.SetInt("Height", resolutionData.Height);
        }
        public void SaveFullscreen(bool value) => PlayerPrefs.SetInt("Fullscreen", value ? 1 : 0);
        public void SaveMusic(bool value) => PlayerPrefs.SetInt("Music", value ? 1 : 0);
        public void SaveSound(bool value) => PlayerPrefs.SetInt("Sound", value ? 1 : 0);
        public void SaveLanguage(string value) => PlayerPrefs.SetString("Language", value);
        public ResolutionData LoadResolution()
        {
            return new ResolutionData(PlayerPrefs.GetInt("Width"), PlayerPrefs.GetInt("Height"));
        }
        public bool LoadFullscreen() => PlayerPrefs.GetInt("Fullscreen") != 0;
        public bool LoadMusic() => PlayerPrefs.GetInt("Music") != 0;
        public bool LoadSound() => PlayerPrefs.GetInt("Sound") != 0;
        public string LoadLanguage() => PlayerPrefs.GetString("Language");

    }
}
