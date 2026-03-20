using UnityEngine;

public class SceneSound : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource ambienceSource;
    public AudioSource musicSource;

    [Header("Audio Clips")]
    public AudioClip ambience;
    public AudioClip music;

    void Start()
    {
        // เสียงบรรยากาศ
        if (ambienceSource != null && ambience != null)
        {
            ambienceSource.clip = ambience;
            ambienceSource.loop = true;
            ambienceSource.Play();
        }

        // เสียงดนตรี
        if (musicSource != null && music != null)
        {
            musicSource.clip = music;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}