using Assets.Scripts.Core;
using Assets.Scripts.Core.Interface.Game;
using Assets.Scripts.Data;

namespace Assets.Scripts.Game.Level
{
    internal class LevelDataProvider : ILevelDataProvider
    {
        private readonly DatasLevel _datasLevel;
        public LevelDataProvider(DatasLevel datasLevel)
        {
            _datasLevel = datasLevel;
            _datasLevel.LoadFromJson();
        }
        public LevelData GetLevel(int index)
        {
            if (!HasLevel(index)) return null;
            return _datasLevel.Levels[index];
        }
        public bool HasLevel(int index)
        {
            if(_datasLevel == null) return false;
            if(_datasLevel.Levels == null) return false;
            return _datasLevel.Levels.Count > index && index >= 0;
        }
    }
}
