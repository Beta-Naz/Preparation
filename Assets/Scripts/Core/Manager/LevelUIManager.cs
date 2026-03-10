using Assets.Scripts.Core.Interface.Game;
using UnityEngine;

namespace Assets.Scripts.Core.Manager
{
    public class LevelUIManager : ILevelUIManager
    {

        private readonly GameObject _winPanel;
        private readonly GameObject _losePanel;
        private readonly GameObject[] _activeObjects;

        public LevelUIManager(GameObject winPanel, GameObject losePanel, GameObject[] activeObjects)
        {
            _winPanel = winPanel;
            _losePanel = losePanel;
            _activeObjects = activeObjects;
        }
        public void ShowWinPanel()
        {
            if (_losePanel != null && _losePanel.activeSelf) _losePanel.SetActive(false);
            if (_winPanel != null) _winPanel.SetActive(true);
            _winPanel?.SetActive(true);
        }
        public void ShowLosePanel()
        {
            if (_winPanel != null && _winPanel.activeSelf) _winPanel.SetActive(false);
            if (_losePanel != null) _losePanel.SetActive(true);
        }
        public void HideAllPanels()
        {
            if(_winPanel != null) _winPanel.SetActive(false);
            if (_losePanel != null)_losePanel.SetActive(false);
            foreach (var obj in _activeObjects)
            {
                if (obj != null) obj.SetActive(false);
            }
        }
    }
}
