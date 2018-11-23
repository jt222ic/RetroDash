using UnityEngine;
using System.Collections;

public class Camerafollow : MonoBehaviour {

    // Use this for initialization
    //public Transform target;
    private PlayerController target;
    Vector3 OriginPosition;
    public GameObject WarpEffect;

    void Start()
    {
    }
    void LateUpdate()
    {
        target = FindObjectOfType<PlayerController>();
        if (target != null)
        {
            if (target.transform.position.y > transform.position.y)
            {
                Vector3 newPos = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
                WarpEffect.SetActive(true);
                transform.position = newPos;
            }
            if(target.transform.position.y < transform.position.y)
            {
                WarpEffect.SetActive(false);
            }
        }
    }
}
