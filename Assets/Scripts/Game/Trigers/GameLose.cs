using Assets.Scripts.Core.Manager;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game.Trigers
{
	public class GameLose: MonoBehaviour
	{
        private LevelManager LevelManager => LevelManager.Instance;
        private InvokeManager InvokeManager => InvokeManager.Instance;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (InvokeManager == null)
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
                if (LevelManager != null)
                {
                    PlayerPrefs.SetInt("CurrentLevel", 0);
                    LevelManager.GameLosePanel.SetActive(true);
                    InvokeManager.IsStart = false;
                }
            }
        }
    }
}