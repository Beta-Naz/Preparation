using Assets.Scripts.Data;

namespace Assets.Scripts.Core.Interface.Game
{
    public interface ILevelDataProvider
    {
        LevelData GetLevel(int index);
        bool HasLevel(int index);
    }
}
