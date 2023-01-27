using UnityEngine;

namespace Quicorax.RuntimeTools
{
    public class SimpleScreenShot : MonoBehaviour
    {
        private const string Platform = "Android";

        private int _index = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                ResolutionScreenShot();
        }

        private void ResolutionScreenShot()
        {
            Debug.Log("ScreenShot!");

            ScreenCapture.CaptureScreenshot($"SacredSplinter_{Platform} _Screenshot_{_index}.png", 4);
            _index++;
        }
    }
}