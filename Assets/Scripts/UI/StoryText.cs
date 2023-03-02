using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class StoryText : MonoBehaviour
{
    [SerializeField] private float delayTime = 1f;
    [TextArea(3, 10)] //https://docs.unity3d.com/2019.4/Documentation/ScriptReference/TextAreaAttribute.html
    [SerializeField] public string storyText;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    /// <summary>
    /// When the player collides with this object, the storyUIpanel appears with the text replaced with this object's
    /// storyText property. If the object the player collides with is tagged as "Finish", when the player clicks
    /// the storyUIButton, then the FinishGameButton_onClick event is called.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            GameManager.instance.triggeredStoryAtScene = true;

            StartCoroutine(DelayStoryUIPanel(delayTime));
            //UIManager.instance.DisplayMessage(storyText);
            GameManager.instance.uiManager.DisplayMessage(storyText);
            if (gameObject.CompareTag("Finish"))
            {
                //UIManager.instance.storyUIButton.onClick.AddListener(FinishGameButton_onClick); //subscribe to the onClick event
                GameManager.instance.uiManager.storyUIButton.onClick.AddListener(FinishGameButton_onClick); //subscribe to the onClick event
            }
        }
    }

    IEnumerator DelayStoryUIPanel(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.instance.StopPlayerInput();
        //GameManager.instance.acceptPlayerInput = false;
    }

    /// <summary>
    /// Loads the main menu.
    /// <a href = "https://answers.unity.com/questions/1448790/change-onclick-function-via-script.html"></a>
    /// </summary>
    void FinishGameButton_onClick()
    {
        Time.timeScale = 1f;
        //UIManager.instance.LoadSceneByName("0_MainMenu");
        GameManager.instance.uiManager.LoadSceneByName("0_MainMenu");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            //if (!UIManager.instance.storyUIPanel.activeInHierarchy)
            if (!GameManager.instance.uiManager)
            {
                gameObject.SetActive(false);
            }
            Time.timeScale = 1f;
        }
    }
}
