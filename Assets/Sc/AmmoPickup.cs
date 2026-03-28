using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("Ammo")]
    public int ammoAmount = 30;

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
        // 🌀 หมุน
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);

        // ✨ ลอยขึ้นลง
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + new Vector3(0f, newY, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunShoot gun = other.GetComponentInChildren<GunShoot>();

            if (gun != null)
            {
                gun.totalAmmo += ammoAmount;
            }

            // 🔊 เล่นเสียงตอนเก็บ (แม้ object จะโดนลบ)
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            Destroy(gameObject);
        }
    }
}