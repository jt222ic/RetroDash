using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageLerp : MonoBehaviour
{

    // Use this for initialization

    public Color lerpedColor;
    public Color Blue;
    Text image;
    void Start()
    {
        image = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float startTime = Time.time;

        while (startTime > Time.time)
        {
            lerpedColor = Color.Lerp(new Color(1, 1, 1, 1), Blue, (startTime) +3 - Time.time);
           
        }
        lerpedColor = Color.Lerp(Color.white, Blue, Mathf.PingPong(Time.time, 1));

        image.color = lerpedColor;
    }

    //private IEnumerator Glow(Color color, float cooloutTime)
    //{
    //    //var _renderer = transform.Find("Marker").GetComponent<SpriteRenderer>();
       
    //}


}


