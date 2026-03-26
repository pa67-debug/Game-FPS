using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    [Header("Effect")]
    public GameObject bloodEffect;
    public float bloodHeight = 1.4f;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip hitSound;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Monster HP: " + currentHealth);

        // เล่นเสียงโดนยิง
        if (hitSound && audioSource)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // สร้างเอฟเฟคเลือด
        if (bloodEffect != null)
        {
            Vector3 bloodPos = transform.position + Vector3.up * bloodHeight;

            GameObject blood = Instantiate(bloodEffect, bloodPos, Quaternion.identity);
            Destroy(blood, 1f); // ลบเอฟเฟคหลัง 1 วินาที
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (WaveManager.instance != null)
        {
            WaveManager.instance.MonsterDied();
        }

        Destroy(gameObject);
    }
}