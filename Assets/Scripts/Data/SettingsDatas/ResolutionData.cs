using System;

namespace Assets.Scripts.Data.SettingsDatas
{
    [Serializable]
    public class ResolutionData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public ResolutionData() { }

        public ResolutionData(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
