using UnityEngine;
using System.Collections;

public class speedBoostplatform : MonoBehaviour {

    // Use this for initialization
    public float speedboost = 3.0f;
    bool SpeedIsOn;
    PlayerController player;
    public AudioSource soundeffect;
    void Start () {
   }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = FindObjectOfType<PlayerController>();
            SpeedIsOn = true;
            soundeffect.Play();
            other.transform.Translate(Vector3.up * speedboost);
            player.SpeedBoostOn(speedboost, SpeedIsOn);
        }
    }
}
