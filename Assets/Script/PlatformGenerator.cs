using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour {

    // Use this for initialization

    public GameObject GeneratePoint;
    //public GameObject[] Platforms;
    float distancebetween = 10f;
   
    Vector3 shiftingposition;
    public ObjectPooler[] objectpooler;
    ObjectPooler OneObjectpool;
    List<GameObject> ObjectCollection;
     public Vector2 PlatformStartPosition;
    int[] selector;
    int Pick_Platform;
    int Pick_randomcoin;
    private CoinGenerator coingenerate;
    private GameController GameMaster;
    float CoinDifferentPosX;
    public float everytime = 80;
    BoxCollider2D getPlatform;
    PolygonCollider2D getPolygon;

    void Start () {
        PlatformStartPosition = transform.position;
        ObjectCollection = new List<GameObject>();
        GeneratePoint = GameObject.Find("GeneratePoint");
        coingenerate = FindObjectOfType<CoinGenerator>();
        GameMaster = FindObjectOfType<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y< GeneratePoint.transform.position.y)
        {
            Pick_Platform = Random.Range(0, objectpooler.Length);
            Pick_randomcoin = Random.Range(0, coingenerate.objectpooler.Length);
            transform.position = new Vector2(transform.position.x,transform.position.y + distancebetween);
            CoinDifferentPosX = Random.Range(0.5f, 5.5f);
            var CoinDifferentPosY = Random.Range(-5f, 5f);
            GameObject newPlatform = objectpooler[Pick_Platform].getPooledObject();
            getPlatform = newPlatform.GetComponentInChildren<BoxCollider2D>();
            getPolygon = newPlatform.GetComponentInChildren<PolygonCollider2D>();
            if (newPlatform != null)
                {
                    newPlatform.transform.position = transform.position;
                    newPlatform.transform.rotation = transform.rotation;
                    newPlatform.SetActive(true);
                    GameObject coin= coingenerate.PoolCoin(new Vector3(transform.position.x+ CoinDifferentPosX, transform.position.y+ CoinDifferentPosY, transform.position.z), Pick_randomcoin);
                  
                if (getPlatform != null && coin.GetComponent<BoxCollider2D>())
                {

                    while (coin.GetComponent<BoxCollider2D>().bounds.Intersects(getPlatform.bounds))
                    {

                        CoinDifferentPosX = Random.Range(0.5f, 5.5f); /// new random position
                        CoinDifferentPosY = Random.Range(-5f, 5f);  // new random position
                       
                        coin.transform.position = new Vector3(transform.position.x + CoinDifferentPosX, transform.position.y + CoinDifferentPosY, transform.position.z);  // new position if coin intersect with platform
                    }
                }
                if (getPlatform != null && coin.GetComponent<CircleCollider2D>())
                {

                    while (coin.GetComponent<CircleCollider2D>().bounds.Intersects(getPlatform.bounds))
                    {
                        CoinDifferentPosX = Random.Range(0.5f, 5.5f); /// new random position
                        CoinDifferentPosY = Random.Range(-5f, 5f);  // new random position

                        coin.transform.position = new Vector3(transform.position.x + CoinDifferentPosX, transform.position.y + CoinDifferentPosY, transform.position.z);  // new position if coin intersect with platform
                    }
                }
                if (getPolygon != null && coin.GetComponent<BoxCollider2D>())
                {
                    while (coin.GetComponent<BoxCollider2D>().bounds.Intersects(getPolygon.bounds))
                    {
                        
                        CoinDifferentPosX = Random.Range(0.5f, 5.5f); /// new random position
                        CoinDifferentPosY = Random.Range(-5f, 5f);  // new random position

                        coin.transform.position = new Vector3(transform.position.x + CoinDifferentPosX, transform.position.y + CoinDifferentPosY, transform.position.z);  // new position if coin intersect with platform
                    }
                }
                if (getPolygon != null && coin.GetComponent<CircleCollider2D>())
                {
                    while (coin.GetComponent<CircleCollider2D>().bounds.Intersects(getPolygon.bounds))
                    {
                        

                        CoinDifferentPosX = Random.Range(0.5f, 5.5f); /// new random position
                        CoinDifferentPosY = Random.Range(-5f, 5f);  // new random position

                        coin.transform.position = new Vector3(transform.position.x + CoinDifferentPosX, transform.position.y + CoinDifferentPosY, transform.position.z);  // new position if coin intersect with platform
                    }
                }
            }

                

            if(GameMaster.score>= everytime)
            {
                everytime += 80;
                AddTraps();
            }
            }
        }
    public void AddTraps()
    {
        Pick_randomcoin = Random.Range(0, coingenerate.OneRotationpooler.Length);
        GameObject rotation = coingenerate.PoolRotation(new Vector3(transform.position.x + CoinDifferentPosX, transform.position.y-3f, transform.position.z), Pick_randomcoin);
    }
    public void DestroyAllObject()
    {
        for (int i = 0; i < 1; i++)
        {
            objectpooler[i].ClearPool();
        }
    }
    public void RecreateList()
    {
        for (int i = 0; i < 1; i++)
        {
            objectpooler[i].AddList();

        }
    }

}

