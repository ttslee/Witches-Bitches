using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Rigidbody2D rb;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player1") || hitInfo.CompareTag("Player2"))
            hitInfo.GetComponent<PlayerController>().TakeDamage();
        if (hitInfo.gameObject.tag != "Resource" && hitInfo.gameObject.tag != "Item")
            Destroy(gameObject);
    }
}
