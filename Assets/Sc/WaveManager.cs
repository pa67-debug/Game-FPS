using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [Header("Wave Settings")]
    public int maxWaves = 15;
    public int currentWave = 0;

    public float startCountdown = 10f;
    public float waveTime = 180f;
    public float breakTime = 10f;

    [Header("Spawn Settings")]
    public float spawnDelay = 0.6f;

    [Header("Monster")]
    public GameObject monsterPrefab;
    public Transform[] spawnPoints;

    [Header("Portal")]
    public GameObject portalPrefab;

    [Header("UI")]
    public TMP_Text waveText;
    public TMP_Text timerText;
    public TMP_Text monsterText;

    [Header("Victory UI")]
    public GameObject victoryPanel;
    public TMP_Text finalTimeText;

    int monstersAlive = 0;

    float gameTime = 0f;
    bool isGameEnded = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        victoryPanel.SetActive(false);
        StartCoroutine(GameLoop());
    }

    void Update()
    {
        if (!isGameEnded)
        {
            gameTime += Time.deltaTime;
        }
    }

    IEnumerator GameLoop()
    {
        yield return StartCoroutine(StartCountdown());

        while (currentWave < maxWaves)
        {
            currentWave++;

            yield return StartCoroutine(StartWave());

            yield return StartCoroutine(WaveTimer());

            if (currentWave < maxWaves)
            {
                yield return StartCoroutine(BreakCountdown());
            }
        }

        EndGame();
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

    IEnumerator StartWave()
    {
        waveText.text = "WAVE " + currentWave;

        int monsterCount = 5 + (currentWave - 1) * 4;

        monstersAlive = monsterCount;
        UpdateMonsterUI();

        for (int i = 0; i < monsterCount; i++)
        {
            SpawnMonster();
            yield return new WaitForSeconds(spawnDelay);
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
                yield break;
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

        Quaternion rot = spawn.rotation * Quaternion.Euler(0, 0, 90);

        GameObject portal = Instantiate(portalPrefab, spawn.position, rot);

        Instantiate(monsterPrefab, spawn.position, Quaternion.identity);

        Destroy(portal, 1f);
    }

    public void MonsterDied()
    {
        if (isGameEnded) return;

        monstersAlive--;
        UpdateMonsterUI();

        if (currentWave == maxWaves && monstersAlive <= 0)
        {
            EndGame();
        }
    }

    void UpdateMonsterUI()
    {
        monsterText.text = "X " + monstersAlive;
    }

    void EndGame()
    {
        if (isGameEnded) return;

        isGameEnded = true;

        victoryPanel.SetActive(true);

        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        finalTimeText.text = "TIME: " + minutes.ToString("00") + ":" + seconds.ToString("00");

        Time.timeScale = 0f;

        // 🔥 แก้ปุ่มกดไม่ได้
        EnableUI();
    }

    void EnableUI()
    {
        // ปลดล็อกเมาส์
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // รีเฟรช EventSystem กันบัค
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    // 🔁 ปุ่ม Replay
    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 🏠 ปุ่ม Menu
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}