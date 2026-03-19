using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    
    [Space(20)]
    [Header("UI Stuff")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject deathScreen;

    void Start()
    {
        score = 0;
        
        healthText.text = $"Health: 10";
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

    public void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }

    public void LoseHealth(int newHealth)
    {
        healthText.text = $"Health: {newHealth}";
    }
}
