using UnityEngine;
using UnityEngine.Video;

namespace Assets.Scripts.UI.Panel
{
	public class PanelVideoPlay: MonoBehaviour
	{
		private VideoPlayer _ads;
		void Start()
		{
            _ads = GetComponent<VideoPlayer>();
        }
	}
}