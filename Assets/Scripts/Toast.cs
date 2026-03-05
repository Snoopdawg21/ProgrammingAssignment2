using UnityEngine;
using TMPro;

public class Toast : MonoBehaviour
{
    public static Toast instance;

    [SerializeField] private GameObject toastUI;
    [SerializeField] private TMP_Text toastText;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);

        instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toastUI.SetActive(false);
    }

    public void ShowToast(string text)
    {
        toastUI.SetActive(true);
        toastText.SetText(text);
    }

    public void HideToast()
    {
        toastUI.SetActive(false);
    }
}
