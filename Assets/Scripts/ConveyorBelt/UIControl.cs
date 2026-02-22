using UnityEngine;

public class UIControl : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Camera mainCam;


    void Update()
    {
        float yMin = mainCam.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        float yMax = mainCam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float x = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(x, yMin, 0));
        lineRenderer.SetPosition(1, new Vector3(x, yMax, 0));
    }
}
