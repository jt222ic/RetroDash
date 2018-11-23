using UnityEngine;
using System.Collections;

public class kicker : MonoBehaviour {


    public PointEffector2D effect;
    float timer;
    float maxRecovery_ = 0.5f;
   private bool ConditionTrue;
    PlayerController playerr;
    int hit;
    void start()
    {
      
        
    }
    void Update()
    {

        if (ConditionTrue)
        {
            
        }
        if (GetComponent<PointEffector2D>().enabled == true)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 3)
        {
            effect.GetComponent<PointEffector2D>().enabled = false;
            
            timer = 0;
           
        }
    }
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
                
                ConditionTrue = true;
                GetComponent<PointEffector2D>().enabled = true;
                hit = 0;
                playerr = FindObjectOfType<PlayerController>();
                playerr.CooltimeTimer(maxRecovery_, ConditionTrue);
            }
    }
}
