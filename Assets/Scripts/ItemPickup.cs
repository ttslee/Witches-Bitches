using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public int holder = 0;
    private bool dropped = false;
    private string direction;
    private SpriteRenderer image;

    private void Start()
    {
        //print(transform.parent);
    }
    private void Awake()
    {
        image = gameObject.GetComponent<SpriteRenderer>();
    }

    public int Holder
    {
        get
        {
            return holder;
        }

        set
        {
            //print(value);
            holder = value;
            image.sortingOrder = 2;
            gameObject.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    // NOT IMPLEMENTED YET
    private void Update()
    {
        if(dropped)
        switch(direction)
        {
            case "up":
                break;
            case "down":
                break;
            case "right":
                break;
            case "left":
                break;
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Drop(string dir)
    {
        Holder = 0;
        direction = dir;
        dropped = true;
        image.sortingOrder = 0;
        gameObject.GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
}
