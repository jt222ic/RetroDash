using UnityEngine;
using System.Collections;

public class PlatformDestroyer : MonoBehaviour {

    // Use this for initialization

    public GameObject DestructionPoint;
	void Start () {

        DestructionPoint = GameObject.Find("DestructionPoint");

	}
	
	// Update is called once per frame
	void Update () {

        if (DestructionPoint != null)
        {
            if (transform.position.y <= DestructionPoint.transform.position.y)
            {

                gameObject.SetActive(false);
            }
        }
	}
}
