using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerController player;
    private int numOfHearts = 3;
    private int health;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start() {
        player = GetComponent<PlayerController>();
        health = player.Health; 

        if (health > numOfHearts) {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++) {
            if (i < numOfHearts) {
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
        }
    }
    void Update() {
        health = player.Health; 

        for (int i = 0; i < hearts.Length; i++) {
            if (i < health) {
                hearts[i].sprite = fullHeart;
            }
            else {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
