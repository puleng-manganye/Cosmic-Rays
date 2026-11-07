using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 *  
 */
#endregion

public class InteractableDoor : MonoBehaviour
{
    #region Fields

    public Animator _animator;
    private GameObject _player;
    public bool doorCloses = false;
    public float doorDelayTime = 5f;

    // Open with Key
    public bool needsKey;
    private Inventory _inventory;
    public Inventory.ItemType keyRequired;

    // Icon
    public GameObject Icon;

    // SFX
    public GameObject sfx;

    #endregion

    #region References and Initializations

    private void Awake()
    {
         _player = GameObject.FindGameObjectWithTag("Player");
        _inventory = _player.GetComponent<Inventory>();
        Icon.gameObject.SetActive(false);
    }
    #endregion

    #region Trigger events

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _player)
        {
            if (_inventory.currentItem != keyRequired)
            {
                Icon.gameObject.SetActive(true);
            }

            if (!needsKey)
            {
                // Open door
                _animator.SetInteger("DoorState", 1);

                // Instantiate effects if there are any.
                #region SFX
                if (sfx != null)
                {
                    Instantiate(sfx, transform.position, Quaternion.identity);
                }
                #endregion
            }
            else if (needsKey && _inventory.currentItem == keyRequired)
            {
                // Open door
                _animator.SetInteger("DoorState", 1);

                // Instantiate effects if there are any.
                #region SFX
                if (sfx != null)
                {
                    Instantiate(sfx, transform.position, Quaternion.identity);
                }
                #endregion
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == _player)
        {
            Icon.gameObject.SetActive(false);
        }

        if (col.gameObject == _player && doorCloses == true)
        {
            StartCoroutine(CloseDelay());
        }
    }
    #endregion

    #region Close delay

    IEnumerator CloseDelay()
    {
        yield return new WaitForSeconds(doorDelayTime);

        // Close door
        _animator.SetInteger("DoorState", 2);

        yield return new WaitForSeconds(1.1f);

        // Return to "Idle" position
        _animator.SetInteger("DoorState", 0);
    }
    #endregion
}
