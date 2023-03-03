using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class inherits from the Pickup class and will give the player extra bombs when picked up
/// </summary>
public class GarBombPickup : Pickup
{
    [Header("Bomb Info")]
    [Tooltip("The amount of bombs that will be given to the player upon collision")]
    [SerializeField] private int amountOfBombs;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            AddBomb(1);
        }
        base.OnTriggerEnter2D(collision);
    }

    /// <summary>
    /// Adds an amount of bomb ammo and updates the UI. If the player is already at max bombs,
    /// the currentGarlicBombCount will not be incremented.
    /// </summary>
    /// <param name="bombCount">The amount of bombs to add.</param>
    private void AddBomb(int bombCount)
    {
        GameManager.CurrentGarlicBombCount += bombCount;
        if (GameManager.CurrentGarlicBombCount > GameManager.MaxGarlicBombCount)
        {
            GameManager.CurrentGarlicBombCount = GameManager.MaxGarlicBombCount;
        }
        GameManager.instance.UpdateUI();
    }
}