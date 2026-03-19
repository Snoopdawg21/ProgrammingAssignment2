using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    public event Action OnPauseEvent;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private MouseHider mh;

    private bool isPaused;
    
    public void OnPause()
    {
        Time.timeScale = isPaused ? 1 : 0;
        
        isPaused = !isPaused;
        
        pauseMenu.SetActive(isPaused);
        mh.ShowMouse(isPaused);
        
        OnPauseEvent?.Invoke();
    }
}
