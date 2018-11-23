using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackAndForth : MonoBehaviour {

    // Use this for initialization
    public float walkspeed;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * walkspeed * Time.deltaTime);
        if(transform.position.x > 2.5f)
        {
            walkspeed--;
        }
        if (transform.position.x < -3f)
        {
            walkspeed++;
        }
    }
}
