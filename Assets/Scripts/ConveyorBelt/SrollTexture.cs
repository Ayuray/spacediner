using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class UIScrollTexture : MonoBehaviour
{
    public float scrollSpeedX = 0.5f;
    public float scrollSpeedY = 0.0f;
    
    private RawImage rawImg;

    void Start()
    {
        rawImg = GetComponent<RawImage>();
    }

    void Update()
    {
        // Calculate the new position for the UV rect
        float x = Time.time * scrollSpeedX;
        float y = Time.time * scrollSpeedY;

        // Apply it to the UV Rect (keeping the original width/height)
        rawImg.uvRect = new Rect(x, y, rawImg.uvRect.width, rawImg.uvRect.height);
    }
}