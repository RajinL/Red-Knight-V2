using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    //[SerializeField] private GameObject storySignBox;
    [Tooltip("Message to display.")]
    [TextArea(3, 10)] //https://docs.unity3d.com/2019.4/Documentation/ScriptReference/TextAreaAttribute.html
    [SerializeField] private string message;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            GameManager.instance.uiManager.DisplayMessage(message);
        }
    }
}
