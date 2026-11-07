using UnityEngine;

#region Class Description:
/*
 * Adds some interaction and makes sure we can click through the story with the button.
 * Calls StoryManager.DisplayNextLine() function on each click.
 */
#endregion

public class ContinueButtonScript : MonoBehaviour
{
    #region Fields

    // A reference to the Story Manager
    private StoryManager _storyManager;
    #endregion

    #region Setting up the Story Manager

    private void Start()
    {
        // Get the Story Manager
        _storyManager = FindObjectOfType<StoryManager>();

        // If Story Manager is not found,
        // display an error mesage
        if (_storyManager == null)
        {
            Debug.LogError("Story Manager was not found!");
        }
    }
    #endregion

    #region When button is cliked

    public void OnClick()
    {
        // Call StoryManager.DisplayNextLine() function on each click
        _storyManager?.DisplayNextLine();
    }
    #endregion 
}