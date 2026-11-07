using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class is placed on items that can be collected by the player, 
 * so that when the player collides with them the item is added to the player's inventory.
 * 
 * NOTES: The object that this script is attached to should have a 2D collider on it
 * with the 'Is Trigger' flag on the component set to TRUE. 
 */
#endregion

public class Item : MonoBehaviour
{
    #region Fields

    [Header("Item")]

    // Item to give
    public Inventory.ItemType itemToGive;

    [Header("Effects")]

    // VFX
    public GameObject vfx; 

    // SFX
    public GameObject sfx;

    // A reference to the Player
    private GameObject _player;
    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion

    #region When the Player collides with the Item

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check to see if the object we're colliding with is the Player, 
        // and if we have remembered to set the type of item!
        if(col.gameObject == _player && itemToGive != Inventory.ItemType.Null)
        {
            //Get a reference to the player's inventory
            Inventory inv = _player.GetComponent<Inventory>();
            

            // Run a loop for each slot in the inventory, if the slot is empty, 
            // fill it with this item, otherwise carry on going until we run out of slots.
            int count = 0;
            foreach(Inventory.ItemType item in inv.inventory)
            {
                if (item == Inventory.ItemType.Null)
                {
                    inv.inventory[count] = itemToGive;
                    if (_player.GetComponent<Inventory>().currentItem == Inventory.ItemType.Null)
                    {
                        _player.GetComponent<Inventory>().currentItem = itemToGive;
                    }

                    #region Effects

                    #region VFX
                    if (vfx != null)
                    {
                        Instantiate(vfx, transform.position, Quaternion.identity);
                    }
                    #endregion

                    #region SFX
                    if (sfx != null)
                    {
                        Instantiate(sfx, transform.position, Quaternion.identity);
                    }
                    #endregion

                    #endregion

                    // Clean up
                    Destroy(gameObject);
                    break;
                }
                count++;
            }
        }
    }
    #endregion
}