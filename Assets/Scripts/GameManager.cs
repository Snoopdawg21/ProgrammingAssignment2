using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AttackManager am;
    
    [SerializeField] private int score;
    private bool boughtFireball;
    private bool boughtSpinSword;
    
    [Space(20)]
    [Header("UI Stuff")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text fireballCost;
    [SerializeField] private TMP_Text spinSwordCost;
    [SerializeField] private GameObject deathScreen;

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

    public void DeadPlayer()
    {
        deathScreen.SetActive(true);
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
            if (score >= 50)
            {
                am.basicAttackType = 2;

                score -= 50;
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

    public void BuySpinSpeed()
    {
        if (boughtSpinSword)
        {
            if (score >= 2)
            {
                am.IncreaseSpinSpeed(10);
                score -= 2;
                UpdateScore();
            }
            else
                Debug.Log("You can't afford that.");
        }
        else
            Debug.Log("You need to buy the spinning sword.");
    }

    public void BuySpinningSword()
    {
        if (!boughtSpinSword)
        {
            if (score >= 10)
            {
                am.BuySpinningSword();
                
                score -= 10;
                boughtSpinSword = true;
                UpdateScore();
                
                spinSwordCost.text = "Unavailable";
                spinSwordCost.color = Color.gray2;
            }
            else
                Debug.Log("You can't afford that.");
        }
        else
            Debug.Log("You already have the spinning sword.");
    }
}
