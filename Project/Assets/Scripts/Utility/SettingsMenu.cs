using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    #region Fields

    // Audio mixer component
    public AudioMixer audioMixer;
    
    // Dropdown UI to display the resolutions
    public Dropdown resolutionDropdown;
    
    // Array to store resolutions
    private Resolution[] _resolutions; 
    #endregion

    #region References and Initializations

    private void Start()
    {
       // Gets an array of all resolutions in Unity
       _resolutions = Screen.resolutions;   
        
       // Clears all defaults in the dropdown UI
       resolutionDropdown.ClearOptions();             

        // To store all resolution options
        List<string> options = new List<string>();    

        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {    // To display the width and height of our resolution
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Add resolution options to dropdown UI
        resolutionDropdown.AddOptions(options);    
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
#endregion

    #region Set resolution

    //This function updates the resolution
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    #endregion

    #region Set volume
    
    //This function is called when we adjust the volume slider.
    public void SetMusicVolume (float volume)
    {
        //Debug.Log(volume);
        audioMixer.SetFloat("MusicVolume", volume);
    }
    
    public void SetSFXVolume (float volume)
    {
        //Debug.Log(volume);
        audioMixer.SetFloat("SFXVolume", volume);
    }
    #endregion

    #region Set quality and Change screen size

    //This function is called when we change the resolution
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //This function is called when we change the screen size.
    public void ChangeScreenSize(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    #endregion
}
