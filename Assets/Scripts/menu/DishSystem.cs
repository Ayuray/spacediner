using System;
using UnityEngine;
using UnityEngine.UI;


public class DishSystem : MonoBehaviour
{
    [SerializeField] GameObject orderReference;

    public static event Action OnOrderEnd;

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
        Debug.Log("order");
        Dish dish = GetComponent<DishGenerator>().GenerateDish();
        orderReference.SetActive(true);
        orderReference.GetComponent<Image>().sprite = dish.icon;
    }

    public void EndOrder()
    {
        orderReference.SetActive(false);

        OnOrderEnd?.Invoke();
    }
}
