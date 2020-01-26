using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    public GameObject itemSpawn;

    
    public Sprite itemFire;
    public Sprite itemDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.name)
        { 
            case "Player1":
            case "Player2":
                if(gameObject.name != "Rock")
                    collision.gameObject.GetComponent<PlayerController>().TakeDamage();
                break;
            case "Fire":
            case "Death":
                itemSpawn.GetComponent<SpriteRenderer>().sprite = (collision.gameObject.name == "Fire") ? itemFire : itemDeath;
                itemSpawn.GetComponent<SpriteRenderer>().enabled = true;
                itemSpawn.GetComponent<CircleCollider2D>().enabled = true;
                Destroy(gameObject);
                break;
               
        }
    }
}
