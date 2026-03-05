using System;

namespace Assets.Scripts.Data.SettingsDatas
{
    [Serializable]
    public class SettingData
    {
        public bool FullScreen;
        public ResolutionData Resolution;
        public bool Music;
        public bool Sound;
        public string Language;
    }
}
