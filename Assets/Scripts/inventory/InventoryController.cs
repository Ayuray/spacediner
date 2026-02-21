using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.Rendering;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]public ItemGrid selectedItemGrid;

    InventoryItem selectedItem;
    RectTransform rectTransform;

    InputAction mousePosition;
    InputAction mouseClick;
    InputAction mouseRightClick;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    private void Start()
    {
        mousePosition = InputSystem.actions.FindAction("Point");
        mouseClick = InputSystem.actions.FindAction("Click");
        mouseRightClick = InputSystem.actions.FindAction("RightClick");
    }
    private void Update()
    {
        DragSelectedItem();

        if (mouseRightClick.WasPressedThisFrame())
        {
            CreateRandomItem();
        }

        if (selectedItemGrid == null) { return; }


        if (mouseClick.WasPressedThisFrame())
        {
            PickOrPlaceItem();
        }
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.SetItem(items[selectedItemID]);
    }

    private void PickOrPlaceItem()
    {
        Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(mousePosition.ReadValue<Vector2>());

        if (selectedItem == null)
        {
            selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
            if (selectedItem != null)
            {
                rectTransform = selectedItem.GetComponent<RectTransform>();
            }
        }
        else
        {
            selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y);
            selectedItem = null;
        }
    }

    private void DragSelectedItem()
    {
        if (selectedItem != null)
        {
            rectTransform.position = mousePosition.ReadValue<Vector2>();
        }
    }
}
