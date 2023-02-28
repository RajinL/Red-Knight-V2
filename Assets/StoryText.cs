using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StoryText : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [TextArea][SerializeField] private string storyText;
    [SerializeField]private float delayTime = 1f;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        uiManager = GameObject.FindGameObjectWithTag("ui_manager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            StartCoroutine(DelayStoryUIPanel(delayTime));
            uiManager.storyUIPanel.SetActive(true);
            uiManager.storyTextUI.text = storyText;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            if (!uiManager.storyUIPanel.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator DelayStoryUIPanel(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0f;
    }
}
