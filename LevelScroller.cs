using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    private float lastPosY=0;
    private float distance;
    private float sync;
    private RectTransform rect;
    public RectTransform canvas;
    private float topBorder;
    private float bottomBorder;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        sync = canvas.rect.height/Screen.height;
        bottomBorder = rect.localPosition.y;
        topBorder = bottomBorder + rect.rect.height-canvas.rect.height;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPosY = Input.mousePosition.y;
            distance = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            distance = Input.mousePosition.y - lastPosY;
            distance *= sync;
            lastPosY = Input.mousePosition.y;

            if (rect.localPosition.y + distance < bottomBorder)
                rect.localPosition = new Vector3(0, bottomBorder, 0);
            else if (rect.localPosition.y + distance > topBorder)
                rect.localPosition = new Vector3(0, topBorder, 0);
            else
                rect.localPosition += new Vector3(0, distance, 0);
            distance = 0;
        }
    }
}
