using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SFXHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private EventReference recipePopUp;
    [SerializeField] private EventReference newCustomer;
    [SerializeField] private EventReference pointGet;
    [SerializeField] private EventReference pointLoose;
    [SerializeField] private EventReference selectChoppingBoard;
    [SerializeField] private EventReference select;
    [SerializeField] private EventReference confirm;
    [SerializeField] private EventReference back;
    [Header("Item")]
    [SerializeField] private EventReference pickUpItem;
    [SerializeField] private EventReference placingItem;
    [SerializeField] private EventReference rotateItem;
    [SerializeField] private EventReference dogBark;


    private void OnMenuPopUp()
    {
        RuntimeManager.PlayOneShot(recipePopUp);
    }

    private void OnNewCustomer()
    {
        RuntimeManager.PlayOneShot(newCustomer);
    }

    private void OnPointGet()
    {
        RuntimeManager.PlayOneShot(pointGet);
    }

    private void OnPointLoose()
    {
        RuntimeManager.PlayOneShot(pointLoose);
    }

    private void OnSelectChoppingBoard()
    {
        RuntimeManager.PlayOneShot(selectChoppingBoard);
    }

    private void OnSelect()
    {
        RuntimeManager.PlayOneShot(select);
    }

    private void OnConfirm()
    {
        RuntimeManager.PlayOneShot(confirm);
    }

    private void OnBack()
    {
        RuntimeManager.PlayOneShot(back);
    }

    private void OnItemPickUp()
    {
        RuntimeManager.PlayOneShot(pickUpItem);
    }

    private void OnPlacingItem()
    {
        RuntimeManager.PlayOneShot(placingItem);
    }

    private void OnRotateItem()
    {
        RuntimeManager.PlayOneShot(rotateItem);
    }
    
    private void OnDogBark()
    {
        RuntimeManager.PlayOneShot(dogBark);
    }
}
