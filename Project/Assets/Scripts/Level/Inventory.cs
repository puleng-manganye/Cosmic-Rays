using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Class Description:
/* 
 * This class defines the types of item the player can pick up, 
 * manages the items already in their inventory,
 * and allows the player to cycle through the items they currently have.
*/
#endregion

public class Inventory : MonoBehaviour
{
    #region Fields

    #region Public Fields:
    [Range(1, 10)]

    // Number of inventory slots.
    public int inventorySize = 4;   

    // Declares an enum containing the types of item that are available for the player to pick up,
    // including an empty, or "Null" type for slots that don't have anything in them currently.
    public enum ItemType                      
    {                             
        Null,                      
        RedKey,                    
        BlueKey,                    
        GreenKey,
        YellowKey,
    }

    // Cycle Inventory Button
    public KeyCode _cycleInventoryButton;

    public string currentInventoryItem;
    // Creates an array for the items to be stored in.
    public ItemType[] inventory;

    public Text item;
    #endregion

    #region Non-serialized Public Fields:

    // Tracks the current item selected in the inventory.
    [System.NonSerialized]
    public ItemType currentItem;
    #endregion

    #region Private Fields:

    private int _itemCount;
    #endregion

    #endregion

    #region References and Initializations 

    private void Awake()
    {
        // Set inventory size.
        inventory = new ItemType[inventorySize];
    }

    private void Start()
    {
        // Set the current inventory item to the 1st item.
        currentItem = inventory[0];
        currentInventoryItem = currentItem.ToString();
    }
    #endregion

    #region Cycle Inventory

    private void Update()
    {
        if (Input.GetKeyDown(_cycleInventoryButton))
        {
            CycleInventory();
        }

        if (currentItem == inventory[0])
        {
            item.text = "Current Inventory Item: Bronze Key";
        }
        else if (currentItem == inventory[1])
        {
            item.text = "Current Inventory Item: Silver Key";
        }
        else if (currentItem == inventory[2])
        {
            item.text = "Current Inventory Item: Gold Key";
        }
    }

    // This block lets us cycle the current item in our inventory,
    // this could be used to set an active item, for example.
    private void CycleInventory()
    {
        if (_itemCount < inventorySize - 1)
        {
            _itemCount++;
            currentItem = inventory[_itemCount];
            currentInventoryItem = currentItem.ToString();
        }
        else
        {
            _itemCount = 0;
            currentItem = inventory[_itemCount];
            currentInventoryItem = currentItem.ToString();
        }
    }
    #endregion
}
