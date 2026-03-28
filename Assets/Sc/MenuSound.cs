using UnityEngine;

public class MenuSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip menuBGM;

    void Start()
    {
        if (audioSource != null && menuBGM != null)
        {
            audioSource.clip = menuBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}