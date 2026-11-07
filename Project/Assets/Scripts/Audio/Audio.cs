using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/*
 * 
 */
#endregion

public class Audio : MonoBehaviour
{
    #region Fields

    // A reference to the Audio Source
    private AudioSource _audioSource;

    // Enable/disable play
    private bool _play;

    // Detect when you use the toggle,
    // ensures audio clip isn’t played multiple times
    private bool _toggleChange;
    #endregion

    #region References and Initializations

    private void Start()
    {
        // Get the Audio Source
        _audioSource = GetComponent<AudioSource>();

        // Ensure the toggle is set to true for the audio clip to play at start-up
        _play = true;
    }
    #endregion

    #region Play audio

    private void Update()
    {
        // Check to see if you just set the toggle to positive
        if (_play == true && _toggleChange == true)
        {
            // Play the audio you attach to the AudioSource component
            _audioSource.Play();

            // Ensure audio doesn’t play more than once
            _toggleChange = true;
        }

        // Check if you just set the toggle to false
        if (_play == false && _toggleChange == true)
        {
            // Stop the audio
            _audioSource.Stop();

            // Ensure audio doesn’t play more than once
            _toggleChange = false;
        }
    }

    private void OnGUI()
    {
        // Switch this toggle to activate and deactivate the parent GameObject
        _play = GUI.Toggle(new Rect(10, 10, 100, 30), _play, "Play Music");

        // Detect if there is a change with the toggle
        if (GUI.changed)
        {
            // Change to true to show that there was just a change in the toggle state
            _toggleChange = true;
        }
    }
    #endregion
}
