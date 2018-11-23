using UnityEngine;
using System.Collections;

public class Transitionwall : MonoBehaviour {

    // Use this for initialization
    public Transform destination;
    public Vector3 offset;

	void Start () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            if (other.transform.position.y <transform.position.y)
            {


                other.transform.position = new Vector3(0, destination.transform.position.y, 0) + offset;

            }
            
        }
     

    }
}
