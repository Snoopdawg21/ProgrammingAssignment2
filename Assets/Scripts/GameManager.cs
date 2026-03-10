using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    
    [SerializeField] private Image healthBar;

    public void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log(score);
    }

    public void LoseHealth(int newHealth)
    {
        
    }
}
