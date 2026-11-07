using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnTrigger : MonoBehaviour
{
    #region Fields

    // A reference to the Audio Source
    public AudioSource audioSource;
    #endregion

    #region When the Player/an object collides with the Trigger

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Play the audio clip attached to the Audio Source when function is called
        audioSource.Play();
    }
    #endregion
}
