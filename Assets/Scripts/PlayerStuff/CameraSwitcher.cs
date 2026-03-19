using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineCamera exploreCam;
    [SerializeField] private CinemachineCamera aimCam;

    [SerializeField] private PlayerController playerController;

    void OnEnable()
    {
        playerController.OnStateUpdated += SwitchCam;
    }

    void OnDisable()
    {
        playerController.OnStateUpdated -= SwitchCam;
    }

    private void SwitchCam(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Explore:
                exploreCam.Prioritize();
                break;
            
            case PlayerState.Aim:
                aimCam.Prioritize();
                break;
            
            default:
                break;
        }
    }
}