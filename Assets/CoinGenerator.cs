using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {


    public ObjectPooler[] objectpooler;

    public ObjectPooler[] OneRotationpooler;
    // Use this for initialization
    void Start () {
		
	}

    public GameObject PoolCoin(Vector3 Startposition, int number)
    {
        GameObject newCoin = objectpooler[number].getPooledObject();
        newCoin.transform.position = Startposition;
        newCoin.SetActive(true);
        

        return newCoin;
    }

    public GameObject PoolRotation(Vector3 Startposition, int number)
    {
        GameObject newCoin = OneRotationpooler[number].getPooledObject();
        newCoin.transform.position = Startposition;
        newCoin.SetActive(true);

        return newCoin;
    }
}
