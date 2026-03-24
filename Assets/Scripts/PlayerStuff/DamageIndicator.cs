using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private RawImage hitIndicator;
    private float fillAmmount;
    private float tempAmmount;
    private float timer;
    
    void Start()
    {
        fillAmmount = 0;
        timer = 0;
    }

    void Update()
    {
        if (timer > 2f)
        {
            ResetColours();
        }

        tempAmmount = fillAmmount;
        hitIndicator.color = new Color(hitIndicator.color.r, hitIndicator.color.g, hitIndicator.color.b, tempAmmount);
        
        timer += Time.deltaTime;
    }
    
    public void TakeDamage()
    {
        if (fillAmmount < 0.75f)
        {
            fillAmmount += 0.2f;
        }
        timer = 0;
    }

    void ResetColours()
    {
        if (fillAmmount >= 0)
        {
            fillAmmount -= 0.05f;
        }
    }
}