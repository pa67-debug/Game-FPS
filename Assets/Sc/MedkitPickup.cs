using UnityEngine;

public class MedkitPickup : MonoBehaviour
{
    [Header("Medkit")]
    public int amount = 1;

    [Header("Rotate")]
    public float rotateSpeed = 100f;

    [Header("Float")]
    public float floatSpeed = 2f;
    public float floatHeight = 0.25f;

    [Header("Sound")]
    public AudioClip pickupSound;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 🌀 หมุนรอบแกน Y
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);

        // ✨ ลอยขึ้นลง
        float y = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + new Vector3(0f, y, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.AddMedkit(amount);
            }

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            Destroy(gameObject);
        }
    }
}