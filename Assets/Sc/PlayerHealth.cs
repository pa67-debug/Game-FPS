using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public Image hpImage;
    public Sprite[] hpSprites;

    public TextMeshProUGUI hpText;

    [Header("Damage Screen")]
    public Image damageImage;

    [Header("Game Over")]
    public GameObject gameOverPanel;

    [Header("UI To Hide")]
    public GameObject gameUI;   // UI ทั้งหมดของเกม (HP / Ammo / Wave)

    void Start()
    {
        currentHealth = maxHealth;

        damageImage.enabled = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

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

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameOver();
        }

        StartCoroutine(DamageFlash());

        UpdateHP();
    }

    void GameOver()
    {
        if (gameUI != null)
            gameUI.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // ปิดเสียง
        AudioListener.pause = true;

        // ปลดล็อกเมาส์
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // หยุดเวลา
        Time.timeScale = 0f;
    }

    IEnumerator DamageFlash()
    {
        damageImage.enabled = true;

        yield return new WaitForSeconds(0.2f);

        damageImage.enabled = false;
    }

    void UpdateHP()
    {
        int index = currentHealth / 10;
        hpImage.sprite = hpSprites[index];

        int percent = Mathf.RoundToInt((float)currentHealth / maxHealth * 100f);
        hpText.text = percent + "%";
    }
}