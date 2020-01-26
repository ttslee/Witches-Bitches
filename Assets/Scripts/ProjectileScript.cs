using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private string parent;
    
    public float speed = 20f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        //Vector2 spd = new Vector2();
        //spd.Set(20, 0);
        //rb.velocity = spd;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //if(hitInfo.name != parent && hitInfo.gameObject.tag != "Item")
            //Destroy(gameObject);
    }
    public void SetProjectile(string p, string dir)
    {
        parent = p;
        Direction(dir);
    }
    public void Direction(string dir)
    {
        Vector2 spd = new Vector2();
        switch(dir)
        {
            case "up":
                spd.Set(0, speed);
                break;
            case "down":
                spd.Set(0, -speed);
                break;
            case "left":
                spd.Set(-speed, 0);
                break;
            case "right":
                spd.Set(speed, 0);
                break;
        }
        rb.velocity = spd;
    }
}
