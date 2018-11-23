using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{

    // Use this for initialization
    GameController game;
    public GameObject firework;
    public SpriteRenderer[] mysprite;
    private ParticleSystem effector;
    //public AudioSource Soundeffect;
    bool GetPoint;
    float time;
    void Start()
    {
        game = FindObjectOfType<GameController>();
        effector = firework.GetComponent<ParticleSystem>();   // get the child object
        time = 0.85f;
        //mysprite = game.GetComponentInParent<SpriteRenderer[]>();
    }

    // Update is called once per frame

    void Update()
    {

        
        if(GetPoint)
        {
            
            time -= Time.deltaTime;
            
            if(time <= 0)
            {
                if (effector.IsAlive(false))
                {
                    firework.SetActive(false);
                }
                for (int i = 0; i < mysprite.Length; i++)
                {
                    mysprite[i].enabled = true;
                }
                gameObject.SetActive(false);
                GetPoint = false;
            }
        }
        }
    
    void OnTriggerEnter2D(Collider2D other)           // on collision getting the game object to detect the player TAg then add points depending on ScoreGive   " IMPORTANT TO WRITE EACH word CORRECTly or Unity wont understand  "OnTriggerEnter2D"
    {

        if (other.gameObject.tag == "Player")
        {

            for (int i = 0; i < mysprite.Length; i++)
            {
                mysprite[i].enabled = false;
            }
            firework.SetActive(true);
            effector.Play();
            game.IncrementScore();
            GetPoint = true;
            time = 0.8f;
        }

    }
}
