using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator anim;

    void Update()
    {
        anim.SetBool("IsGrounded", playerController.IsGrounded());
        anim.SetFloat("Velocity", playerController.GetPlayerVelocity().sqrMagnitude);
    }

    void OnEnable()
    {
        playerController.OnJumpEvent += OnJump;
    }

    void OnDisable()
    {
        playerController.OnJumpEvent -= OnJump;
    }

    private void OnJump()
    {
        anim.SetTrigger("Jump");
    }
}