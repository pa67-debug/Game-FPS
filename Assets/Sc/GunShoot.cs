using UnityEngine;
using System.Collections;
using TMPro;

public class GunShoot : MonoBehaviour
{
    public Camera cam;
    public Transform muzzle;
    public LineRenderer tracer;
    public RectTransform crosshair;

    public Animator anim;

    [Header("Gun")]
    public float range = 1000f;
    public int damage = 25;

    [Header("Ammo")]
    public int maxAmmo = 30;
    public int currentAmmo;
    public int totalAmmo = 90;

    bool isReloading = false;

    public float reloadTime = 3f;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip emptySound;

    [Header("UI")]
    public TextMeshProUGUI ammoText;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        UpdateAmmoUI(); // อัปเดตกระสุน

        if (isReloading) return;

        // Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        // Shoot
        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo <= 0)
            {
                if (emptySound != null)
                    audioSource.PlayOneShot(emptySound);

                anim.ResetTrigger("Shoot");
                return;
            }

            Shoot();
        }
    }

    void Shoot()
    {
        currentAmmo--;

        anim.SetTrigger("Shoot");

        if (shootSound != null)
            audioSource.PlayOneShot(shootSound);

        UpdateAmmoUI();

        Ray ray = cam.ScreenPointToRay(crosshair.position);

        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, range))
        {
            targetPoint = hit.point;

            MonsterHealth monster = hit.collider.GetComponentInParent<MonsterHealth>();

            if (monster != null)
            {
                monster.TakeDamage(damage);
            }
        }
        else
        {
            targetPoint = ray.GetPoint(range);
        }

        StartCoroutine(ShowTracer(muzzle.position, targetPoint));
    }

    IEnumerator Reload()
    {
        if (totalAmmo <= 0 || currentAmmo == maxAmmo)
            yield break;

        isReloading = true;

        anim.SetTrigger("Reload");

        if (reloadSound != null)
            audioSource.PlayOneShot(reloadSound);

        yield return new WaitForSeconds(reloadTime);

        int need = maxAmmo - currentAmmo;

        if (totalAmmo >= need)
        {
            currentAmmo = maxAmmo;
            totalAmmo -= need;
        }
        else
        {
            currentAmmo += totalAmmo;
            totalAmmo = 0;
        }

        UpdateAmmoUI();

        isReloading = false;
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo + " / " + totalAmmo;
        }
    }

    IEnumerator ShowTracer(Vector3 start, Vector3 end)
    {
        tracer.SetPosition(0, start);
        tracer.SetPosition(1, end);
        tracer.enabled = true;

        yield return new WaitForSeconds(0.05f);

        tracer.enabled = false;
    }
}