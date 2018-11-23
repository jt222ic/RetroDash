using UnityEngine;
using System.Collections;

public class rotationClock : MonoBehaviour {

    // Use this for initialization
    float rotationsPerMinute = 40f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.forward * rotationsPerMinute * Time.deltaTime);
    }
}
