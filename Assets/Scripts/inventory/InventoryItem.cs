using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData itemData;

    public int Height
    {
        get
        {
            if (rotated)
            {
                return itemData.width;
            }
            return itemData.height;
        }
    }

    public int Width
    {
        get
        {
            if (rotated)
            {
                return itemData.height;
            }
            return itemData.width;
        }
    }

    public int onGridPositionX;
    public int onGridPositionY;

    public bool rotated = false;


    internal void SetItem(ItemData itemData)
    {
        this.itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2();
        size.x = Width * ItemGrid.tileSizeWidth;
        size.y = Height * ItemGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }
    internal void Rotate()
    {
        rotated = !rotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated ? 90f : 0f);
    }
}
