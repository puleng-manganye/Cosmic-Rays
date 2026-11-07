using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Class Description:
/*
 * 
 */
#endregion

public class LoopAudio : MonoBehaviour
{
    #region Fields

    // A reference to the Audio Source
    public AudioSource audioSource;
    #endregion

    #region Loop audio

    private void Start()
    {
        // Play the audio clip attached to the Audio Source
        audioSource.Play();

        // Loop audio clip
        audioSource.loop = true;
    }
    #endregion
}
