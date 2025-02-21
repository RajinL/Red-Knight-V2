﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            collision.gameObject.transform.SetParent(transform);
        }

        if (collision.gameObject.CompareTag("non_player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject  == GameManager.instance.player)
        {
            collision.gameObject.transform.SetParent(null);
        }

        if (collision.gameObject.CompareTag("non_player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

}
