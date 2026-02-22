using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;


public class DishSystem : MonoBehaviour
{
    [SerializeField] GameObject orderReference;

    public static event Action OnOrderEnd;

    Dish dish;

    private void OnEnable()
    {
        WaitingQueue.OnOrderStart += StartOrder;
    }

    private void OnDisable()
    {
        WaitingQueue.OnOrderStart -= StartOrder;
    }

    private void Start()
    {
        orderReference.SetActive(false);
    }
    public void StartOrder()
    {
        dish = GetComponent<DishGenerator>().GenerateDish();
        orderReference.SetActive(true);
        orderReference.GetComponent<Image>().sprite = dish.icon;
    }

    public Dish GetCurrentOrder()
    {
        return dish;
    }

    public void EndOrder()
    {
        orderReference.SetActive(false);

        OnOrderEnd?.Invoke();
    }
}
