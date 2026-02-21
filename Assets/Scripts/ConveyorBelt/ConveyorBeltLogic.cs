using System;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using UnityEditor.Experimental;

public class ConveyorBeltLogic : MonoBehaviour
{
    [System.Serializable]
    public class ConveyorBeltItem
    {
        public Transform item;
        [HideInInspector] public float currentLerp;
        [HideInInspector] public int startPoint;
    }

    public static event Action<Transform, ConveyorBeltItem> OnConveyorEnd;
    [SerializeField] private float itemSpacing;
    [SerializeField] private float speed;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private List<ConveyorBeltItem> _items;

    void Start()
    {
        OnConveyorEnd += teleportItem;
    }
    
    private void FixedUpdate()
    {
        int maxItems = _items.Count;
        float lineLength = Vector3.Distance(_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(1));
        itemSpacing = lineLength / maxItems;
        
        for (int i = 0; i < _items.Count; i++)
        {
            ConveyorBeltItem beltItem = _items[i];
            Transform item = beltItem.item;

            if (i > 0)
            {
                if(Vector3.Distance(item.position, _items[i - 1].item.position) <= itemSpacing)
                    continue;
            }
            
            item.transform.position = Vector3.Lerp(_lineRenderer.GetPosition(beltItem.startPoint), _lineRenderer.GetPosition(beltItem.startPoint + 1), beltItem.currentLerp);
            float distance = Vector3.Distance(_lineRenderer.GetPosition(beltItem.startPoint), _lineRenderer.GetPosition(beltItem.startPoint + 1));
            beltItem.currentLerp += (speed * Time.deltaTime)/distance;

            if (beltItem.currentLerp >= 1)
            {
                if (beltItem.startPoint + 2 < _lineRenderer.positionCount)
                {
                    beltItem.currentLerp = 0;
                    beltItem.startPoint++;
                }
                //OnConveyorEnd?.Invoke(item, beltItem);
                item.transform.position = _lineRenderer.GetPosition(beltItem.startPoint);
                beltItem.currentLerp = 0;
            }
        }
    }

    private void teleportItem(Transform item, ConveyorBeltItem beltItem)
    {
        item.transform.position = _lineRenderer.GetPosition(beltItem.startPoint);
    }
}
