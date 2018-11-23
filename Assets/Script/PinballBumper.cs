using UnityEngine;
using System.Collections;

public class PinballBumper : MonoBehaviour
{

    // Use this for initialization

    
    float maxRecovery = 0.6f;
    bool IfTrue;
    Animator anim;

    PlayerController player;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        player = FindObjectOfType<PlayerController>();
        maxRecovery -= Time.deltaTime;
        if (other.gameObject.tag == "Player")
        {
            other.rigidbody.AddRelativeForce(-other.contacts[0].normal *7.5f,ForceMode2D.Impulse);
            IfTrue = true;
            anim.SetTrigger("Trigger");
            player.CooltimeTimer(maxRecovery, IfTrue);
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        anim.SetTrigger("isTrigger");
    }







}
