using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 30;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunShoot gun = other.GetComponentInChildren<GunShoot>();

            if (gun != null)
            {
                gun.totalAmmo += ammoAmount;
            }

            Destroy(gameObject);
        }
    }
}