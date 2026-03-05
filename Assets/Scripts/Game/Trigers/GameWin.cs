using Assets.Scripts.Core.Manager;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    private LevelManager _levelManager => LevelManager.Instance;
    private InvokeManager _invokeManager => InvokeManager.Instance;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_invokeManager == null)
        {
            Debug.LogError("_invokeManager == null", this);
            return;
        }
        if (!_invokeManager.IsStart)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            if(_levelManager != null)
            {
                int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0) + 1;
                PlayerPrefs.SetInt("CurrentLevel", currentLevel);
                Debug.Log($"Current level = {currentLevel}");
                _levelManager.GameWinPanel.SetActive(true);
                _invokeManager.IsStart = false;
            }
        }
    }
}
