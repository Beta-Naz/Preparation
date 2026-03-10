using Assets.Scripts.Core.Manager;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    private LevelManager LevelManager => LevelManager.Instance;
    private InvokeManager InvokeManager => InvokeManager.Instance;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(InvokeManager == null)
        {
            Debug.LogError("_invokeManager == null", this);
            return;
        }
        if (!InvokeManager.IsStart)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            if(LevelManager != null)
            {
                int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0) + 1;
                PlayerPrefs.SetInt("CurrentLevel", currentLevel);
                Debug.Log($"Current level = {currentLevel}");
                LevelManager.UiLevelManager.ShowWinPanel();
                InvokeManager.IsStart = false;
            }
        }
    }
}
