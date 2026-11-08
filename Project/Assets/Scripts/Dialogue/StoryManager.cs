using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#region Class Description:
/*
 * This class allows the Player to engage in conversations with NPCs using Ink.
 */
#endregion

public class StoryManager : MonoBehaviour
{
    #region Fields

    // A reference to the Story File
    [SerializeField]
    private TextAsset _inkJsonAsset;
    // The Story Class
    private Story _story;

    [SerializeField]
    // A reference to the text field where we display our story
    private Text _textField, _nameField;

    [SerializeField]
    // Different fonts
    private Font _DefaultFont, _NalediFont, _CompanionFont, _SizweFont;

    // A reference to the Vertical Layout Group 
    [SerializeField]
    private HorizontalLayoutGroup _choiceButtonContainer;
    // A reference to the ChoiceButton prefab
    [SerializeField]
    private Button _choiceButtonPrefab, _continueButton;

    // A reference  to the background image of the name text
    public GameObject _nameBG, _namePanel, _dialoguePanel, _pausePrompt;

    // Colours for each character's Name
    [SerializeField]
    private Color Naledi, Companion, Sizwe, noColor;

    // Speech Bubbles
    [SerializeField]
    private SpriteRenderer _Naledi, _Companion, _Sizwe;

    // String to store the name of NPC that is talking with Player
    public string activeNPC = "";

    // To check if NPC is already having a dialogue with Player
    public bool activeDialogue = false;

    // A reference to the Level Manager
    private LevelManager _levelManager;

    // SFX
    public GameObject sfx, next_sfx;
    #endregion

    #region Variable

    // Changing variable
    private int _money;
    

    public int Money
    {
        get => _money;
        private set
        {
            //Debug.Log($"Updating Money value. Old value: {_money}, new value: {value}");
            _money = value;
        }
    }
    #endregion

    private void Start()
    {
        #region References and Initializations
        // Get the Level Manager
        _levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();

        //_characterManager = FindObjectOfType<CharacterManager>();
        #endregion

        #region Start our story

        StartStory();
        #endregion

        #region Variable which changes as the story progresses

        var money = (int)_story.variablesState["money"];
        //Debug.Log($"Logging ink variable. Money: {money}");
        #endregion
    }

    private void StartStory()
    {
        #region A way to start our story

        // Use the story file to create an instance of the Story class
        _story = new Story(_inkJsonAsset.text);

        // Destroy all the objects inside the button container
        RefreshChoiceView(); 
        
        // Display the lines of our story
        DisplayNextLine();

        // Will handle variable updates
        InitializeVariables();
    #endregion
    }

    private void InitializeVariables()
    {
        #region Will handle variable updates

        Money = (int)_story.variablesState["money"];
        //Debug.Log($"Logging ink variable. Money: {Money}");

        _story.ObserveVariable("money", (arg, value) => 
        {
            // Instantiate effects if there are any.
            #region Effects

            #region SFX
            if (sfx != null)
            {
                Instantiate(sfx, transform.position, Quaternion.identity);
            }
            #endregion

            #endregion

            //Update candy amount
            _levelManager.AddCandyToBag((int)value);

            //Debug.Log($"Value updated. Money: {value}"); 
        });
        #endregion
    }

    public void DisplayNextLine()
    {
        #region A way to display the lines of our story

        Color currentColor = Color.white;

        // To check if the story can continue before actually using _story.Continue() to get the next line
        if (_story.canContinue)
        {
            _pausePrompt.gameObject.SetActive(false);

            // Gets next line
            string text = _story.Continue();

            // Removes white space from text
            text = text?.Trim();

            // Call our function before we display each line
            ApplyStyling();

            // Displays new text
            _textField.text = text;

            // Instantiate effects if there are any.
            #region Effects

            #region SFX
            if (next_sfx != null)
            {
                Instantiate(next_sfx, transform.position, Quaternion.identity);
            }
            #endregion

            #endregion

            #region Tags
            List<string> tags = _story.currentTags;

            if (tags.Contains("Me")) // 1
            {
                _continueButton.gameObject.SetActive(true);
                _namePanel.gameObject.SetActive(true);
                _dialoguePanel.gameObject.SetActive(true);
                _pausePrompt.gameObject.SetActive(false);

                _nameField.text = "Naledi: ";
                currentColor = Naledi;

                _Naledi.enabled = true;
                _Companion.enabled = false;
                _Sizwe.enabled = false;
            }
            else if (tags.Contains("Companion")) // 2
            {
                _continueButton.gameObject.SetActive(true);
                _namePanel.gameObject.SetActive(true);
                _dialoguePanel.gameObject.SetActive(true);
                _pausePrompt.gameObject.SetActive(false);
                _nameField.text = "Companion: ";
                currentColor = Companion;

                _Naledi.enabled = false;
                _Companion.enabled = true;
                _Sizwe.enabled = false;
            }
            else if (tags.Contains("Sizwe")) // 3
            {
                _continueButton.gameObject.SetActive(true);
                _namePanel.gameObject.SetActive(true);
                _dialoguePanel.gameObject.SetActive(true);
                _pausePrompt.gameObject.SetActive(false);
                _nameField.text = "Sizwe: ";
                currentColor = Sizwe;

                _Naledi.enabled = false;
                _Companion.enabled = false;
                _Sizwe.enabled = true;
            }
            else
            {
                _continueButton.gameObject.SetActive(false);
                _namePanel.gameObject.SetActive(false);
                _dialoguePanel.gameObject.SetActive(false);
                _pausePrompt.gameObject.SetActive(true);
                _nameField.text = "";
                currentColor = noColor;
            }
            #endregion

            #region Variables
            /*_story.ObserveVariable("choice", (string varName, object newValue) => {
                Debug.Log("Choice was made: " + newValue);
            });*/
            #endregion
        }
        // if we can’t continue, it might be because we’re facing some choices
        else if (_story.currentChoices.Count > 0)
        {
            _continueButton.gameObject.SetActive(false);
            currentColor = Naledi;
            _textField.text = "";
            // create and display those choices
            DisplayChoices();
        }
        else
        {   
            activeDialogue = false;
            //Debug.Log("Convo is over.");
            
            // displays new text
            _textField.text = "";

            _namePanel.gameObject.SetActive(false);
            _dialoguePanel.gameObject.SetActive(false);
            _continueButton.gameObject.SetActive(false);
            _pausePrompt.gameObject.SetActive(true);
            _nameField.text = "";
            currentColor = noColor;

            _Naledi.enabled = false;
            _Companion.enabled = false;
            _Sizwe.enabled = false;
        }

        _nameBG.GetComponent<Image>().color = currentColor;

        #endregion
    }

    private void ApplyStyling()
    {
        #region Change fonts for characters

        List<string> tags = _story.currentTags;


        if (tags.Contains("Me")) // 1
        {
            _textField.font = _NalediFont;
        }
        else if (tags.Contains("Companion")) // 2
        {
            _textField.font = _CompanionFont;
        }
        else if (tags.Contains("Sizwe")) // 3
        {
            _textField.font = _SizweFont;
        }
        else
        {
            _textField.font = _DefaultFont;
        }
        #endregion
    }

    private void DisplayChoices()
    {
        #region A way to create and display the choices

        // Check if choices are already being displayed
        if (_choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0) return;

        // Iterate through all of the available choices 
        for (int i = 0; i < _story.currentChoices.Count; i++)
        {
            // Each available choice
            var choice = _story.currentChoices[i];
            // Create a button for each choice
            var button = CreateChoiceButton(choice.text);

            // We add an OnClick event listener to run when the player presses a choice button
            button.onClick.AddListener(() => OnClickChoiceButton(choice));

            if(i==0)
            {
                StartCoroutine(SelectButton(button.gameObject));
            }
        }
        #endregion
    }

    private IEnumerator SelectButton(GameObject button)
    {
        #region Select Button

        yield return null;
        EventSystem.current.SetSelectedGameObject(button);
        #endregion
    }

    private Button CreateChoiceButton(string text)
    {
        #region A way to create a new button from the prefab

        // Creates the button from a prefab
        var choiceButton = Instantiate(_choiceButtonPrefab);

        // Insert it into our container
        choiceButton.transform.SetParent(_choiceButtonContainer.transform, false);

        // Sets text on the button
        var buttonText = choiceButton.GetComponentInChildren<Text>();
        buttonText.text = text;

        // We return the button back to our DisplayChoices() function, where we assign the OnClick event handler
        return choiceButton;
        #endregion
    }

    private void OnClickChoiceButton(Choice choice)
    {
        #region Upon clicking a Choice Button

        // Tell Ink which choice was clicked
        _story.ChooseChoiceIndex(choice.index);

        // Continue with our story
        _story.Continue();

        // Destroy all the old content and choices
        RefreshChoiceView();

        // Display the next lines in our story
        DisplayNextLine();
        #endregion
    }

    private void RefreshChoiceView()
    {
        #region Destroys all the old content and choices

        // After clicking one of the choices, we need to remove all of them from the screen
        if (_choiceButtonContainer != null)
        {
            // Destroy all the objects inside the button container
            foreach (var button in _choiceButtonContainer.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }
        #endregion
    }

    private void Update()
    {
        #region Click enter to advance dialogue

        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Checking which NPC we're talking to
            if (activeNPC == "")
            {
                //Debug.Log("There's no one to talk to");
            } 
            else
            // Start a conversation with an NPC if we're talking to no one
            {
                if (!activeDialogue)
                {
                    _story.ChoosePathString(activeNPC);
                    //Debug.Log("Starting convo with " + activeNPC);

                    activeDialogue = true;
                }

                // Display the next line of dialogue
                DisplayNextLine();
            }
        }
        #endregion
    }
}