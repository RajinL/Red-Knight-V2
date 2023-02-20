using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBomb : MonoBehaviour
{
    public ParticleSystem bombTrail;
    public Transform bombDrop;
    public GameObject bombPrefab;
    public UIManager uiManager;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("ui_manager") != null)
        {
            uiManager = GameObject.FindGameObjectWithTag("ui_manager").GetComponent<UIManager>();
        }
    }

    private void Start()
    {
        if (uiManager != null)
        {
            uiManager.SetPlayerBombCount(GameManager.currentGarlicBombCount);
        }
        else
        {
            Debug.LogWarning("UI Manager cannot be found. Make sure that a UI Canvas tagged with \"ui_manager\" is present");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (GameManager.currentGarlicBombCount > 0)
            {
                DropBomb();
            }
        }
    }

    void DropBomb()
    {
        Instantiate(bombPrefab, bombDrop.position, bombDrop.rotation);
        CreateBombTrail();
        GameManager.currentGarlicBombCount--;
        uiManager.SetPlayerBombCount(GameManager.currentGarlicBombCount);
    }
    void CreateBombTrail()
    {
        if (bombTrail != null)
        {
            bombTrail.Play();
        }
    }
}
