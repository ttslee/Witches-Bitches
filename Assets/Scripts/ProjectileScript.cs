using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private string parent;
    
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Bat") | hitInfo.CompareTag("Rat") | hitInfo.CompareTag("Skeleton"))
        {
            if (hitInfo.tag == "Bat")
                ResourceSpawn.num_bat -= 1;
            if (hitInfo.tag == "Rat")
                ResourceSpawn.num_rat -= 1;
            if (hitInfo.tag == "Skeleton")
                ResourceSpawn.num_skeleton -= 1;
            Destroy(hitInfo.gameObject);
        }
        if (hitInfo.name != parent && hitInfo.gameObject.tag != "Item")
            Destroy(gameObject);
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
                spd.Set(0, 10);
                break;
            case "down":
                spd.Set(0, -10);
                break;
            case "left":
                spd.Set(-10, 0);
                break;
            case "right":
                spd.Set(10, 0);
                break;
        }
        rb.velocity = spd;
        Destroy(gameObject, 1);
    }
}
