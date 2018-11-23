using UnityEngine;
using System.Collections;

public class Moving_Platform_Left : MonoBehaviour {

    // Use this for initialization
    public float walkspeed;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x < -Screen.width)
        {
            TeleportRight(screenPos);
        }
        transform.Translate(Vector3.right * walkspeed * Time.deltaTime);
    }

    void TeleportRight(Vector3 screenPos)
    {
        Vector3 goalScrPos = new Vector3(Screen.width, screenPos.y, screenPos.z);
        Vector3 ScreenToWorldPos = Camera.main.ScreenToWorldPoint(goalScrPos);
        transform.position = ScreenToWorldPos;
    }
}
