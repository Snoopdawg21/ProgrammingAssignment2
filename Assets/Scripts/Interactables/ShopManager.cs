using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private AttackManager am;

    private bool boughtFireball;
    private bool boughtSpinSword;

    private int money;
    
    [SerializeField] private TMP_Text fireballCost;
    [SerializeField] private TMP_Text spinSwordCost;
    [SerializeField] private GameObject spinSwordUpgradeButton;
    [SerializeField] private GameObject spinSwordLengthButton;

    void Start()
    {
        if (am == null)
            am = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackManager>();
        
        if (gm == null)
            gm = gameObject.GetComponent<GameManager>();
    }
    
    public void IncreaseFireRate()
    {
        money = gm.score;
        
        if (money >= 5) 
        { 
            am.fireSpeed++; 
            gm.score -= 5;
            gm.UpdateScore();
        }
        else 
            Debug.Log("You can't afford that.");

        Debug.Log($"New Fire Rate: {am.fireSpeed}");
    }

    public void FireballUpgrade()
    {
        money = gm.score;
        
        if (!boughtFireball)
        {
            if (money >= 25)
            {
                am.basicAttackType = 2;

                gm.score -= 25;
                boughtFireball = true;
                fireballCost.text = "Unavailable";
                fireballCost.color = Color.gray2;
                gm.UpdateScore();
            }
            else
                Debug.Log("You can't afford that.");
        }
        else
            Debug.Log("You already have the fireball.");
    }

    public void BuySpinSpeed()
    {
        money = gm.score;
        
        if (boughtSpinSword)
        {
            if (money >= 2)
            {
                am.IncreaseSpinSpeed(10);
                gm.score -= 2;
                gm.UpdateScore();
            }
            else
                Debug.Log("You can't afford that.");
        }
        else
            Debug.Log("You need to buy the spinning sword.");
    }

    public void BuySpinningSword()
    {
        money = gm.score;
        
        if (!boughtSpinSword)
        {
            if (money >= 10)
            {
                am.BuySpinningSword();
                
                gm.score -= 10;
                boughtSpinSword = true;
                gm.UpdateScore();
                
                spinSwordCost.text = "Unavailable";
                spinSwordCost.color = Color.gray2;
                spinSwordUpgradeButton.SetActive(true);
                spinSwordLengthButton.SetActive(true);
            }
            else
                Debug.Log("You can't afford that.");
        }
        else
            Debug.Log("You already have the spinning sword.");
    }

    public void BuySwordLength()
    {
        money = gm.score;

        if (money >= 5)
        {
            am.spinningSword.transform.localScale = new Vector3(am.spinningSword.transform.localScale.x, am.spinningSword.transform.localScale.y, am.spinningSword.transform.localScale.z + 0.25f);
            gm.score -= 5;
            gm.UpdateScore();
        }
        else
        {
            Debug.Log("You can't afford that.");
        }
    }
}
