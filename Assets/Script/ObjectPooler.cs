using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

    // Use this for initialization

    public int PoolAmount;
    public GameObject Poolobject;
    List<GameObject> PooledObjects;
    public static ObjectPooler SharedInstance;


    private void Awake()
    {
        SharedInstance = this;
    }
    void Start () {

        PooledObjects = new List<GameObject>();
        for (int i= 0; i< PoolAmount; i++)
        {
            GameObject obj2 = (GameObject)Instantiate(Poolobject);
            obj2.SetActive(false);
            PooledObjects.Add(obj2);
        }
	
        
	}
    public GameObject getPooledObject()                                          // to access pooledobject which is inactive in the list
    {
       
        for (int i = 0; i < PooledObjects.Count; i++)
        {

            if (!PooledObjects[i].activeInHierarchy)            // active within the scene 
            {

                return PooledObjects[i];              // return an active PoolObject   // finally it is working // IMPORTANT DO NOT HAVE ANY PREFAB ON THE SCENE, THE ACTIVE IN HIERARCHY WILL SEARCH FOR THAT OBJECT and different name

            } 
        }
        GameObject obj1 = (GameObject)Instantiate(Poolobject);
        obj1.SetActive(false);
        PooledObjects.Add(obj1);
        return obj1;
    }
    public void ClearPool()
    {
        
            for (int i = 0; i < PooledObjects.Count; i++)   // does not work all the active pool dissapear
            {
                GameObject obs = PooledObjects[i];
                Destroy(obs);
            }
    }

    public void AddList()
    {
        PooledObjects = new List<GameObject>();
    }
}
