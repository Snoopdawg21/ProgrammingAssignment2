using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AttackManager am;
    
    [SerializeField] private int score;
    private bool boughtFireball;
    
    [Space(20)]
    [Header("UI Stuff")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text fireballCost;

    void Start()
    {
        score = 0;
        
        healthText.text = $"Health: 10";
        
        if (am == null)
            am = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackManager>();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }

    public void LoseHealth(int newHealth)
    {
        healthText.text = $"Health: {newHealth}";
    }

    public void IncreaseFireRate()
    {
        if (score >= 5) 
        { 
            am.fireSpeed++; 
            score -= 5;
            UpdateScore();
        }
        else 
            Debug.Log("You can't afford that.");

        Debug.Log($"New Fire Rate: {am.fireSpeed}");
    }

    public void FireballUpgrade()
    {
        if (!boughtFireball)
        {
            if (score >= 5)
            {
                am.basicAttackType = 2;

                score -= 5;
                boughtFireball = true;
                fireballCost.text = "Unavailable";
                fireballCost.color = Color.gray2;
                UpdateScore();
            }
            else
                Debug.Log("You can't afford that.");
        }
        else
            Debug.Log("You already have the fireball.");
    }
}
