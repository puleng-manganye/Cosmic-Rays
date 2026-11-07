using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/*
 * 
 */
#endregion

public class PlayAudio : MonoBehaviour
{
    #region Fields

    // A reference to the Audio Source
    public AudioSource audioSource;
    #endregion

    #region Play audio

    public void playThisSFX()
    {
       // Play the audio clip attached to the Audio Source when function is called
       audioSource.Play();
    }
    #endregion
}
