using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;

    public void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log(score);
    }
}
