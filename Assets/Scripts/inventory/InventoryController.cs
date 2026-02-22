using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine.Networking;

public class InventoryController : MonoBehaviour
{
    [HideInInspector] private ItemGrid selectedItemGrid;

    public ItemGrid SelectedItemGrid
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            inventoryHighlight.SetParent(value);
        }
    }

    InventoryItem selectedItem;
    InventoryItem overlapItem;
    InventoryItem itemToHighlight;

    RectTransform rectTransform;

    InputAction mousePosition;
    InputAction mouseClick;
    InputAction mouseRightClick;
    InputAction rotateButton;

    InventoryHighlight inventoryHighlight;

    Vector2Int oldPosition;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;
    [SerializeField] GameObject mainItemGrid;
    [SerializeField] DishSystem dishSystem;
    [SerializeField] DishGenerator dishList;
    [SerializeField] Text text;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    private void Start()
    {
        mousePosition = InputSystem.actions.FindAction("Point");
        mouseClick = InputSystem.actions.FindAction("Click");
        mouseRightClick = InputSystem.actions.FindAction("RightClick");
        rotateButton = InputSystem.actions.FindAction("Rotate");
    }
    private void Update()
    {
        DragSelectedItem();

        if (rotateButton.WasPressedThisFrame())
        {
            RotateItem();
        }

        if (mouseRightClick.WasPressedThisFrame())
        {
            CreateRandomItem();
        }

        if (selectedItemGrid == null)
        {
            inventoryHighlight.Show(false);
            return;
        }

        HandleHighlight();

        if (mouseClick.WasPressedThisFrame())
        {
            PickOrPlaceItem();
        }
    }

    public void ReplaceRandomItem(ItemGrid gridToInsert)
    {
        foreach (Transform child in gridToInsert.transform)
        {
            if (!child.CompareTag("Highlighter"))
            {
                Destroy(child.gameObject);
            }
        }

        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();

        inventoryItem.GetComponent<RectTransform>().SetParent(canvasTransform);

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.SetItem(items[selectedItemID]);

        gridToInsert.PlaceItem(inventoryItem, 1, 1);
    }

    private void RotateItem()
    {
        if (selectedItem == null) { return; }

        selectedItem.Rotate();
        oldPosition = new Vector2Int(0, 0);
    }

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridMousePosition();
        if (oldPosition == positionOnGrid) { return; }
        oldPosition = positionOnGrid;

        if (selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if (itemToHighlight != null)
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
            }
        }
        else
        {
            inventoryHighlight.Show(selectedItemGrid.BoundaryCheck(positionOnGrid.x, positionOnGrid.y, selectedItem.Width, selectedItem.Height));
            inventoryHighlight.SetSize(selectedItem);
            inventoryHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
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
        Vector2Int tileGridPosition = GetTileGridMousePosition();

        if (selectedItem == null)
        {
            selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
            if (selectedItem != null)
            {
                rectTransform = selectedItem.GetComponent<RectTransform>();
                selectedItem.transform.SetParent(canvasTransform);
                selectedItem.transform.SetAsLastSibling();
            }
        }
        else
        {
            bool successful = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);

            if (successful)
            {
                selectedItem = null;
                if (overlapItem != null)
                {
                    selectedItem = overlapItem;
                    overlapItem = null;
                    rectTransform = selectedItem.GetComponent<RectTransform>();
                    selectedItem.transform.SetParent(selectedItemGrid.transform);
                    selectedItem.transform.SetAsLastSibling();
                }
            }
        }
    }

    private Vector2Int GetTileGridMousePosition()
    {
        Vector2 position = mousePosition.ReadValue<Vector2>();

        if (selectedItem != null)
        {
            position.x -= (selectedItem.Width - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.Height - 1) * ItemGrid.tileSizeHeight / 2;
        }

        return selectedItemGrid.GetTileGridPosition(position); ;
    }

    private void DragSelectedItem()
    {
        if (selectedItem != null)
        {
            rectTransform.position = mousePosition.ReadValue<Vector2>();
        }
    }

    public void SendIt()
    {
        int score = 0;

        foreach (Transform child in mainItemGrid.transform)
        {

            if (!child.CompareTag("Highlighter"))
            {
                //ItemData item = child.GetComponent<ItemData>();
                //Dish dish = dishSystem.GetCurrentOrder();
                //
                //List<Dish> dishList = this.dishList.ReturnDishList();
                //
                //
                //
                //foreach (Dish dsh in dishList)
                //{
                //    if (dsh == dish)
                //    {
                //        foreach(CellRequirement ing in dsh.requirements)
                //        {
                //            score += ing.score;
                //        }
                //    }
                //}

                Destroy(child.gameObject);

                score++;
            }
        }

        if (score == 0)
        {
            return;
        }

        text.text = "SCORE: " + (score * 10);
        dishSystem.EndOrder();
    }
}
