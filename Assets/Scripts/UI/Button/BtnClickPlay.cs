using Assets.Scripts.Core.Base;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Button
{
	public class BtnClickPlay: BaseButton
	{
        protected override void OnClick()
        {
            InvokeManager.Instance.IsStart = false;
            InvokeManager.Instance.ClearMassiv();
            SceneManager.LoadScene("PlayLevel");
        }
	}
}