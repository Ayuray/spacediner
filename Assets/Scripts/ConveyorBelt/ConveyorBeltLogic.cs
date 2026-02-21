using System;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;

public class ConveyorBeltLogic : MonoBehaviour
{
    [System.Serializable]
    public class ConveyorBeltItem
    {
        public Transform item;
        [HideInInspector] public float currentLerp;
        [HideInInspector] public int startPoint;
        //public GameObject _PrefabItem;
    }

    public static event Action<Transform, ConveyorBeltItem> OnConveyorEnd;
    [SerializeField] private float itemSpacing;
    [SerializeField] private float speed;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private List<ConveyorBeltItem> _items;
    [SerializeField] private int itemCount;
    [SerializeField] private GameObject _PrefabItem;

    void Start()
    {
        OnConveyorEnd += teleportItem;
        _items = new List<ConveyorBeltItem>(itemCount);
        for (int i = 0; i < itemCount; i++)
        {
            _items.Add(new ConveyorBeltItem());
            _items[i].item = Instantiate(_PrefabItem, transform, false).transform;
        }
    }
    
    private void FixedUpdate()
    {
        CheckAmountOfItems();
        float lineLength = Vector3.Distance(_lineRenderer.GetPosition(0), _lineRenderer.GetPosition(1));
        itemSpacing = lineLength / itemCount;
        
        
        for (int i = 0; i < _items.Count; i++)
        {
            ConveyorBeltItem beltItem = _items[i];
            Transform item = beltItem.item;

            if (CheckItemSpacing(i, item))
                return;
            
            
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
                OnConveyorEnd?.Invoke(item, beltItem);
            }
        }
    }

    private void teleportItem(Transform item, ConveyorBeltItem beltItem)
    {
        item.transform.position = _lineRenderer.GetPosition(beltItem.startPoint);
        beltItem.currentLerp = 0;
    }

    public void SetItemCount(int newCount)
    {
        itemCount = newCount;
    }

    private void CheckAmountOfItems()
    {
        if (itemCount == _items.Count)
            return;
        if (itemCount < _items.Count)
        {
            Destroy(_items[_items.Count - 1].item.gameObject);
            _items.RemoveAt(_items.Count - 1);
        }
        else
        {
            _items.Add(new ConveyorBeltItem());
            _items[_items.Count - 1].item = Instantiate(_PrefabItem, transform, false).transform;
        }
    }

    private bool CheckItemSpacing(int i, Transform item)
    {
        bool reset = false;
        if (i > 0)
        {
            if (Vector3.Distance(item.position, _items[i - 1].item.position) <= itemSpacing)
                reset = true;
            else
            {
                item.position = _items[i - 1].item.position-new Vector3(0, itemSpacing, 0);
            }
        }

        return reset;
    }
}
