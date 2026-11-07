using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region Class Description:
/* 
 * This class stores level related variables and the Player's score. 
 * It also stores data and runs functions related to Checkpoints.
 */
#endregion

public class LevelManager : MonoBehaviour
{
    #region Fields

    #region Public Fields:
    [Header("Tracking")]

    // The current scene
    public string currentScene;

    // The current score
    public int score;
    public Text scoreText;

    // The current candy corn amount
    public int candyCornAmount;
    public Text candyCornText;


    [Header("Enemy Stats")]

    // Jumping
    public int jumpingEnemyDamage = 1;

    // Shooting
    public int shootingEnemyDamage = 2;

    // Walking
    public int walkingEnemyDamage = 1;
    #endregion

    #region Checkpoints:
    [Header("Prefab References")]

    /*
     * Tooltip:
     * Array of Prefab types for the items you want to respawn when the player dies after having reached a checkpoint.
     * Note that this field only reads the object's tag, so if two objects have the same tag, 
     * you don't need to drag both of them into the array.
    */

    // The tags of objects I want to respawn
    public GameObject[] checkpointRespawnTags;
    #endregion

    #region Non-Serialized Public Fields:

    // Current Checkpoint
    [System.NonSerialized]
    public GameObject currentCheckPoint;

    // Score at last Checkpoint
    [System.NonSerialized]
    public int scoreAtLastCheckPoint;

    // Position at last Checkpoint
    [System.NonSerialized]
    public Transform transformAtLastCheckPoint;
    #endregion

    #region Private Fields

    // A reference to the Player
    private GameObject _player;

    // A dictionary to store all the positions of the gameObjects to be respawned
    private Dictionary<int, GameObject[]> _respawns;
    #endregion

    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");
        transformAtLastCheckPoint = _player.transform;

        // Initialize score
        scoreAtLastCheckPoint = score;

        // Get the current scene
        currentScene = SceneManager.GetActiveScene().name;

        // Update the checkpoint system
        _respawns = new Dictionary<int, GameObject[]>();
        UpdateRespawns();

        // Score text
        scoreText.text = score.ToString("0000");

        // Candy Corn amount
        candyCornText.text = candyCornAmount.ToString();
    }
    #endregion

    #region Add points to the Player's score

    // Called by other scripts
    // Adds points to the Player's score
    public void AddPointsToScore(int points)
    {
        score += points;
    }

    // Called by other scripts
    // Adds candy to the Player's bag
    public void AddCandyToBag(int candy)
    {
        candyCornAmount += candy;
    }
    #endregion

    #region Reload the Current Scene

    // Reloads the current scene
    public void ReloadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    #endregion

    #region Update Checkpoint system

    // Updates the checkpoint system
    public void UpdateRespawns()
    {
        // This loop finds all the gameObjects of a specific type (coins, powerups, etc. defined by tags)
        // and stores their positions in a dictionary so that they can be respawned when the player dies
        for (var x = 0; x < checkpointRespawnTags.Length; x++)
        {
            string tag = checkpointRespawnTags[x].tag;
            _respawns[x] = GameObject.FindGameObjectsWithTag(tag);
        }

        //Debug.Log("1st Array of respawn's length: "  + _respawns[0].Length);
    }
    #endregion

    #region Updates

    public void Update()
    {
        // Update score
        scoreText.text = score.ToString("0000");

        // Update Candy Corn
        candyCornText.text = candyCornAmount.ToString();
    }
    #endregion
}
