using UnityEngine;

public class PotionStoreManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameManager gm;

    private int money;

    void Start()
    {
        if (playerController == null)
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void IncreaseHealthBasic()
    {
        money = gm.score;

        if (money >= 5)
        {
            gm.score -= 5;
            playerController.Heal(1);
            gm.UpdateScore();
        }
    }

    public void IncreaseSpeedBasic()
    {
        money = gm.score;

        if (money >= 5)
        {
            gm.score -= 5;
            playerController.IncreaseSpeed(0.5f);
            gm.UpdateScore();
        }
    }
}
