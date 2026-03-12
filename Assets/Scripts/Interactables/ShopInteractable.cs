using System;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, IIInteractable
{
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private MouseHider mh;
    private bool shopIsOpen = false;

    private void Start()
    {
        if(shopMenu == null)
            shopMenu = GameObject.Find("ShopMenu");
    }

    public void OnInteract() 
    {
        shopIsOpen = !shopIsOpen;
        
        shopMenu.SetActive(shopIsOpen);
        mh.ShowMouse(shopIsOpen);
        
        Toast.instance.HideToast();
    }
    
    public void OnHoverIn() 
    {
        if(!shopIsOpen)
            Toast.instance.ShowToast("Press \"E\" to interact");
        
        Debug.Log("In");
    }
    
    public void OnHoverOut() 
    {
        if(shopIsOpen)
            OnInteract();
        
        Toast.instance.HideToast();
        
        Debug.Log("Out");
    }
}
