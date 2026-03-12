using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AttackManager am;
    
    [Space(20)]
    [Header("UI Stuff")]
    [SerializeField] private int score;
    [SerializeField] private Image healthBar;

    void Start()
    {
        score = 0;
        
        if (am == null)
            am = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackManager>();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log(score);
    }

    public void LoseHealth(int newHealth)
    {
        healthBar.fillAmount = newHealth;
    }

    public void IncreaseFireRate()
    {
        if (score > 5)
        {
            am.fireSpeed++;

            score -= 5;
        }
    }
}
