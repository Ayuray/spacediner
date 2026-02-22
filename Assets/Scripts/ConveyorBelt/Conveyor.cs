using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public class ConveyorBeltItemGrid
    {
        [HideInInspector] public GameObject item;
        [HideInInspector] public float currentLerp;
        [HideInInspector] public float previousLerp;
    }

    [SerializeField] private InventoryController inventoryController;

    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private float speed;
    [SerializeField] private float itemCount;
    [SerializeField] private GameObject itemGridPrefab;

    private List<ConveyorBeltItemGrid> itemGrids = new();

    private float currentLerp;
    private float itemSpacing;

    private void Start()
    {
        CheckAmountOfItems();
    }

    private void Update()
    {
        if (itemGrids.Count == 0) return;

        float spacing = 1f / itemGrids.Count;

        for (int i = 0; i < itemGrids.Count; i++)
        {
            float lerp = (Time.time * speed + i * spacing) % 1f;

            itemGrids[i].currentLerp = lerp;

            if (itemGrids[i].currentLerp < itemGrids[i].previousLerp || itemGrids[i].previousLerp == 0)
            {
                inventoryController.ReplaceRandomItem(itemGrids[i].item.GetComponent<ItemGrid>());
            }

            itemGrids[i].previousLerp = itemGrids[i].currentLerp;

            itemGrids[i].item.transform.position =
                Vector3.Lerp(
                    lineRenderer.GetPosition(0),
                    lineRenderer.GetPosition(1),
                    lerp
                );
        }
    }


    public void SetItemCount(int newCount)
    {
        itemCount = newCount;
    }

    private void CheckAmountOfItems()
    {
        if (itemGrids.Count < itemCount)
        {
            ConveyorBeltItemGrid conveyorBeltItemGrid = new()
            {
                item = Instantiate(itemGridPrefab),
                currentLerp = 0

            };

            itemGrids.Add(conveyorBeltItemGrid);
            conveyorBeltItemGrid.item.transform.SetParent(lineRenderer.transform);
            CheckAmountOfItems();
        }
        else if (itemGrids.Count > itemCount)
        {
            Destroy(itemGrids[itemGrids.Count - 1].item.gameObject);
            itemGrids.RemoveAt(itemGrids.Count - 1);
            CheckAmountOfItems();
        }
    }

    //private bool CheckItemSpacing(ConveyorBeltItemGrid currentItem)
    //{
    //    float spacing = 1f / itemCount;
    //
    //    foreach (var other in itemGrids)
    //    {
    //        if (other == currentItem) continue;
    //
    //        if (other.currentLerp > currentItem.currentLerp)
    //        {
    //            float distance = other.currentLerp - currentItem.currentLerp;
    //
    //            if (distance < spacing)
    //            {
    //                Debug.Log(distance);
    //                return false;
    //            }
    //        }
    //    }
    //    return true;
    //}

}
