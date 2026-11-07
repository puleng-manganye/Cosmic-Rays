using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioSource audioSource;

    public void Start()
    {
        Play();
    }

    public void Play()
    {
        audioSource.loop = true;
        audioSource.Play();
    }
    
    public void Stop()
    {
        audioSource.Stop();
    }
}
