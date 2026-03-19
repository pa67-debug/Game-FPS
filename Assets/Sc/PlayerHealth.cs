using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public Image hpImage;

    public Sprite[] hpSprites;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHP();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHP();
    }

    void UpdateHP()
    {
        int index = currentHealth / 10;
        hpImage.sprite = hpSprites[index];
    }
}