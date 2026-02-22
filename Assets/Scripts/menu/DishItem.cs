using System;
using UnityEngine;
using UnityEngine.UI;

public class DishItem : MonoBehaviour
{
    public ItemData dishData;

    public int Height
    {
        get{ return dishData.height;}
    }

    public int Width
    {
        get
        { return dishData.width;}
    }


    internal void SetDish(ItemData dishData)
    {
        this.dishData = dishData;

        GetComponent<Image>().sprite = dishData.itemIcon;

        Vector2 size = new Vector2();
        size.x = Width * ItemGrid.tileSizeWidth;
        size.y = Height * ItemGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }
}
