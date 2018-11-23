using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    // Use this for initialization

    public float walkspeed;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        transform.Translate(Vector3.down * walkspeed * Time.deltaTime);
    }
}
