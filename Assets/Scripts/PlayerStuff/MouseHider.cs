using UnityEngine;

public class MouseHider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowMouse(false);
    }

    public void ShowMouse(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.Confined : CursorLockMode.Locked;
    }
}
