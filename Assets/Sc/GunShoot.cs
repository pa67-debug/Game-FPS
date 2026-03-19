using UnityEngine;
using System.Collections;

public class GunShoot : MonoBehaviour
{
    public Camera cam;
    public Transform muzzle;
    public LineRenderer tracer;
    public RectTransform crosshair;

    public float range = 100f;
    public int damage = 25;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(crosshair.position);

        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, range))
        {
            targetPoint = hit.point;

            MonsterHealth monster = hit.collider.GetComponentInParent<MonsterHealth>();

            if (monster != null)
            {
                int finalDamage = damage;

                if (hit.collider.CompareTag("Body"))
                {
                    finalDamage = damage - 10;
                }
                else if (hit.collider.CompareTag("Head"))
                {
                    finalDamage = damage;
                }

                monster.TakeDamage(finalDamage);
            }
        }
        else
        {
            targetPoint = ray.GetPoint(range);
        }

        StartCoroutine(ShowTracer(muzzle.position, targetPoint));
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