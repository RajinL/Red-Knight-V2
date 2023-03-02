using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that allows the player to throw a bomb, and updates the UI ammo count.
/// </summary>
// Garlic Bomb requires the GameObject to have BombDamage and ObjectPool components
[RequireComponent(typeof(BombDamage))]
[RequireComponent(typeof(ObjectPool))]
public class GarlicBomb : MonoBehaviour
{
    [Header("Garlic Bomb Settings")]
    [Tooltip("The location the bomb is turned on at.")]
    [SerializeField] private Transform bombDrop;

    void Update()
    {
        HandleBombThrow();
    }

    /// <summary>
    /// Upon pressing "Fire2", if the player has at least 1 garlic bomb left, the player
    /// will throw a bomb, and the bomb's ammo count will be updated.
    /// </summary>
    private void HandleBombThrow()
    {
        if (GameManager.instance.acceptPlayerInput && !GameManager.instance.isPlayerDead)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (GameManager.CurrentGarlicBombCount > 0)
                {
                    ThrowBomb();
                    UpdateAmmoCount(1);
                }
            }
        }
    }

    /// <summary>
    /// Uses the Object Pool Script. This function sets each individual pooled
    /// object's position and rotation to this transform, and sets each object
    /// as active. The BombDamage class handles the behaviour of the bomb.
    /// </summary>
    void ThrowBomb()
    {
        GameObject bomb = GetComponent<ObjectPool>().GetPooledObject();
        if (bomb != null)
        {
            bomb.transform.SetPositionAndRotation(bombDrop.transform.position, bombDrop.transform.rotation);
            bomb.SetActive(true);
        }
    }
    
    /// <summary>
    /// Subtracts an amount of bomb ammo and updates the UI
    /// </summary>
    /// <param name="ammoToTakeAway">The amount of bombs to subtract.</param>
    private void UpdateAmmoCount(int ammoToTakeAway)
    {
        GameManager.CurrentGarlicBombCount -= ammoToTakeAway;
        GameManager.instance.UpdateUI();
    }
}
