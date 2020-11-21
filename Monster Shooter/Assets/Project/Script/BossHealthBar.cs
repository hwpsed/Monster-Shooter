using UnityEngine.UI;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{ 
    public float maxHealth;
    public Text healthText;

    private float health;
    private Image healthBar;
    

    void Start()
    {
        healthBar = GetComponent<Image>();
        health = maxHealth;
    }

    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
        if (health <= 0)
            health = 0;
    }
}
