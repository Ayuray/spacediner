using UnityEngine;
using UnityEngine.UI;


public class DishSystem : MonoBehaviour
{
    [SerializeField] GameObject orderReference;

    private void Start()
    {
        orderReference.SetActive(false);
        StartOrder();
    }
    public void StartOrder()
    {
        Dish dish = GetComponent<DishGenerator>().GenerateDish();
        if (dish == null)
        {
            Debug.Log("test");
        }
        orderReference.SetActive(true);
        Debug.Log(dish.dishName);
        orderReference.GetComponent<Image>().sprite = dish.icon;
    }
}
