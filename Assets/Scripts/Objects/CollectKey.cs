using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    public bool hasKey = false;

    private UIManager uiManager;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("ui_manager") != null)
        {
            uiManager = GameObject.FindGameObjectWithTag("ui_manager").GetComponent<UIManager>();
        }
    }

    /// <summary>
    /// If Player collides with this key object, the key will be updated
    /// in the game manager and display on the UI. It will then be set
    /// inactive
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // TO DO
            // update the game manager that a key has been collected
            // and update the UI with the gm
            hasKey = true;
            UpdateKeyCount(1);
            AudioManagerScript.PlaySound("treasureChest");
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Subtracts an amount of bomb ammo and updates the UI
    /// </summary>
    /// <param name="amountToAdd">The amount of bombs to subtract.</param>
    private void UpdateKeyCount(int amountToAdd)
    {
        GameManager.CurrentKeyCount += amountToAdd;
        uiManager.SetPlayerKeyCount();
    }
}
