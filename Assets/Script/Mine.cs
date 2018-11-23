using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {
    GameController game;
    PlayerController player;
    // Use this for initialization
    void Start () {
        game = FindObjectOfType<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter2D(Collider2D other)           // on collision getting the game object to detect the player TAg then add points depending on ScoreGive   " IMPORTANT TO WRITE EACH word CORRECTly or Unity wont understand  "OnTriggerEnter2D"
    {
        if (other.gameObject.tag == "Player")
        {
            player = FindObjectOfType<PlayerController>();
            player.Death();
            game.ConfirmPlayerKill();
            //gameObject.SetActive(false);
        }
    }
}

