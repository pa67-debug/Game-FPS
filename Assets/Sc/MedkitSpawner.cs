using UnityEngine;
using System.Collections;

public class MedkitSpawner : MonoBehaviour
{
    [Header("Medkit")]
    public GameObject medkitPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Time")]
    public float spawnInterval = 60f; // ทุก 1 นาที

    GameObject currentMedkit;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            SpawnMedkit();
        }
    }

    void SpawnMedkit()
    {
        // ❗ ถ้ายังมีอยู่ในแมพ → ไม่ spawn ซ้ำ
        if (currentMedkit != null) return;

        if (spawnPoints.Length == 0 || medkitPrefab == null) return;

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        currentMedkit = Instantiate(medkitPrefab, point.position, Quaternion.identity);
    }
}