using Assets.Scripts.Core.Base;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Button
{
    public class BtnToMainMenu :BaseButton
    {
        protected override void OnClick()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}