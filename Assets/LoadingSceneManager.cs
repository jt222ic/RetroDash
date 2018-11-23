using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour {



    public GameObject Loadinscreen;
    public Slider slider;

    AsyncOperation SynchroSummon;


    private void Start()
    {
        StartCoroutine("LoadingLevel");

    }

    IEnumerator LoadingLevel()
    {

        Loadinscreen.SetActive(true);
        SynchroSummon = SceneManager.LoadSceneAsync(1);
        SynchroSummon.allowSceneActivation = false;


        while(SynchroSummon.isDone == false)
        {
            slider.value = SynchroSummon.progress/ 0.9f;

            if(SynchroSummon.progress == 0.9f)
            {
                slider.value = 1;
                SynchroSummon.allowSceneActivation = true;
            }
            yield return null;
        }
       
    }
}
