using UnityEngine;
using TMPro;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [Header("Wave Settings")]
    public int maxWaves = 15;
    public int currentWave = 0;

    public float startCountdown = 10f;
    public float waveTime = 180f;
    public float breakTime = 10f;

    [Header("Monster")]
    public GameObject monsterPrefab;
    public Transform[] spawnPoints;

    [Header("UI")]
    public TMP_Text waveText;
    public TMP_Text timerText;

    int monstersAlive = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        yield return StartCoroutine(StartCountdown());

        while (currentWave < maxWaves)
        {
            currentWave++;

            StartWave();

            yield return StartCoroutine(WaveTimer());

            if (currentWave < maxWaves)
            {
                yield return StartCoroutine(BreakCountdown());
            }
        }

        timerText.text = "ALL WAVES CLEARED";
    }

    IEnumerator StartCountdown()
    {
        float timer = startCountdown;

        while (timer > 0)
        {
            timerText.text = "START IN: " + Mathf.Ceil(timer);
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    void StartWave()
    {
        waveText.text = "WAVE " + currentWave;

        int monsterCount = 5 + (currentWave - 1) * 4;

        monstersAlive = monsterCount;

        for (int i = 0; i < monsterCount; i++)
        {
            SpawnMonster();
        }
    }

    IEnumerator WaveTimer()
    {
        float timer = waveTime;

        while (timer > 0)
        {
            timerText.text = "TIME: " + Mathf.Ceil(timer);

            if (monstersAlive <= 0)
            {
                break;
            }

            timer -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator BreakCountdown()
    {
        float timer = breakTime;

        while (timer > 0)
        {
            timerText.text = "NEXT WAVE IN: " + Mathf.Ceil(timer);
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    void SpawnMonster()
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(monsterPrefab, spawn.position, Quaternion.identity);
    }

    public void MonsterDied()
    {
        monstersAlive--;
    }
}