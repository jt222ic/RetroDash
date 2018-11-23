using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    // Use this for initialization

   public Transform targetTeleport;
    PlayerController player;
    bool hasTeleport;
    float timer = 1.5f;
    float count = 0;
    public AudioSource teleportSound;
   
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        if(hasTeleport)
        {
            count += Time.deltaTime;

            if(count>=1.7f)
            {
                hasTeleport = false;
                count = 0;
            }
        }
	}
    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("Player"))
        {

            if (!hasTeleport)
            {
                count++;
                otherObject.gameObject.transform.position = targetTeleport.transform.position;
                teleportSound.Play();

                player = FindObjectOfType<PlayerController>();
            }
        }
    }
    void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("Player"))
        {
            hasTeleport = true;
            player.TeleportEffectOn(timer,hasTeleport);
        }
    }
}
