using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    
    [Space(20)]
    [Header("UI Stuff")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject winScreen;
    private bool wonGame;
    private float winScreenTimer;
    private float aValue;

    void Start()
    {
        score = 0;
        
        healthText.text = $"Health: 10";
    }

    void Update()
    {
        if (wonGame)
            winScreenTimer += Time.deltaTime;

        if (winScreenTimer > 2f)
            winScreen.SetActive(false);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScore();
    }

    public void DeadPlayer()
    {
        deathScreen.SetActive(true);
        
        Time.timeScale = 0;
    }

    public void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }

    public void DisplayHealth(int newHealth)
    {
        healthText.text = $"Health: {newHealth}";
    }

    public void WonGame()
    {
        score += 50;
        winScreen.SetActive(true);
        winScreenTimer = 0;
        wonGame = true;
        UpdateScore();
    }
}
