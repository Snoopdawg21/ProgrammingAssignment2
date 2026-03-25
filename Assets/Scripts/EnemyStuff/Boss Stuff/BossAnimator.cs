using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    [SerializeField] private BossEnemyController bec;
    [SerializeField] Animator anim;
    
    void Update()
    {
        anim.SetBool("isHunting", bec.IsHunting());
    }
}
