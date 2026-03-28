using UnityEngine;
using System.Collections;

public class AmmoSpawner : MonoBehaviour
{
    [Header("Ammo")]
    public GameObject ammoPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Time")]
    public float spawnInterval = 60f; // 1 นาที

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            SpawnAmmo();
        }
    }

    void SpawnAmmo()
    {
        if (spawnPoints.Length == 0 || ammoPrefab == null) return;

        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(ammoPrefab, point.position, Quaternion.identity);
    }
}