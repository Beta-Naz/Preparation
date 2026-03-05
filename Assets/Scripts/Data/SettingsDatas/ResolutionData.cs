using System;
using UnityEngine;

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
        /// <summary>
        /// Метод для преобразования переменные width и height в строку "1920:1080"
        /// </summary>
        /// <returns>Возращает строку "1920:1080"</returns>
        public string ResolutionToString()
        {
            return $"{Width}:{Height}";
        }
        /// <summary>
        /// Метод для преобразования строки "1920:1080" в отдельные переменные width и height
        /// </summary>
        /// <param name="resolutionString">строка формат, которой "1920:1080"</param>
        public void StringToResolution(string resolutionString)
        {
            string[] mas = resolutionString.Trim().Split(':');
            if (mas.Length == 2)
            {
                if (int.TryParse(mas[0], out int width) && int.TryParse(mas[1], out int height))
                {
                    Width = width;
                    Height = height;
                    return;
                }
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is ResolutionData other)
            {
                return Width == other.Width && Height == other.Height;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Width, Height);
        }
    }
}
