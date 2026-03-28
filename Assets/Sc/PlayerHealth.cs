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

    [Header("Medkit")]
    public int medkitCount = 0;
    public int healAmount = 25;
    public TextMeshProUGUI medkitText;

    [Header("Heal Sound")]
    public AudioSource audioSource;
    public AudioClip healSound;

    [Header("Damage Screen")]
    public Image damageImage;

    [Header("Game Over")]
    public GameObject gameOverPanel;

    [Header("UI To Hide")]
    public GameObject gameUI;

    void Start()
    {
        currentHealth = maxHealth;

        damageImage.enabled = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateHP();
        UpdateMedkitUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }

        // 💊 กด E ใช้ยา + เล่นเสียง
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseMedkit();
        }
    }

    public void AddMedkit(int amount)
    {
        medkitCount += amount;
        UpdateMedkitUI();
    }

    void UseMedkit()
    {
        if (medkitCount <= 0) return;
        if (currentHealth >= maxHealth) return;

        medkitCount--;

        currentHealth += healAmount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        // 🔊 เล่นเสียงฮีล
        if (healSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(healSound);
        }

        UpdateHP();
        UpdateMedkitUI();
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

        AudioListener.pause = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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

    void UpdateMedkitUI()
    {
        if (medkitText != null)
        {
            medkitText.text = "X" + medkitCount;
        }
    }
}