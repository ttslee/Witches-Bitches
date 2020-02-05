using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController playerOne;
    public PlayerController playerTwo;
    public Sprite deadPlayer;

    void FixedUpdate() {
        if (playerTwo.Health <= 0 && playerOne.Health <= 0) {
            GameOver();
        }
        else if (playerTwo.Health <= 0) {
            PlayerDeath(playerTwo);
        }
        else if (playerOne.Health <= 0) {
            PlayerDeath(playerOne);
        }
    }

    void GameOver() {
        SceneManager.LoadScene("Lose");
    }

    void PlayerDeath(PlayerController player) {
        if (player.Health <= 0) {
            SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
            playerSprite.sprite = deadPlayer;
        }
    }
}
