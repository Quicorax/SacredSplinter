using UnityEngine;

public class SimpleScreenShot : MonoBehaviour
{
    private string Platform = "Generic";
    private int Index = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
            ResolutionScreenShot();
    }

    void ResolutionScreenShot()
    {
        Debug.Log("ScreenShot!");
        ScreenCapture.CaptureScreenshot("AmanitaBoy_"+ Platform + "_Screenshot_"+ Index + ".png", 4);
        Index++;
    }
}
