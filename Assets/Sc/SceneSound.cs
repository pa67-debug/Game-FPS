using UnityEngine;

public class SceneSound : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource ambienceSource;
    public AudioSource musicSource;

    [Header("Audio Clips")]
    public AudioClip ambience;
    public AudioClip music;

    void Awake()
    {
        // 🔥 รีเซ็ตเสียงกันบัค (สำคัญมาก)
        AudioListener.pause = false;
        AudioListener.volume = 1f;
    }

    void Start()
    {
        PlayAll();
    }

    void PlayAll()
    {
        // เสียงบรรยากาศ
        if (ambienceSource != null && ambience != null)
        {
            ambienceSource.Stop(); // 🔥 กันบัค
            ambienceSource.clip = ambience;
            ambienceSource.loop = true;
            ambienceSource.Play();
        }

        // เสียงดนตรี
        if (musicSource != null && music != null)
        {
            musicSource.Stop(); // 🔥 กันบัค
            musicSource.clip = music;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}