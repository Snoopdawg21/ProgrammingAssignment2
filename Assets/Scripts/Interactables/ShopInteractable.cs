using System;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, IIInteractable
{
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private MouseHider mh;
    private bool shopIsOpen = false;

    [SerializeField] private Animator anim;

    private void Start()
    {
        if(shopMenu == null)
            shopMenu = GameObject.Find("ShopMenu");

        if (mh == null)
            mh = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseHider>();
        
        if(anim == null)
            anim = GetComponent<Animator>();
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
    }
    
    public void OnHoverOut() 
    {
        if(shopIsOpen)
            OnInteract();
        
        Toast.instance.HideToast();
        
        anim.SetTrigger("Left");
    }
}
