using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundController : MonoBehaviour {

    public AudioClip[] music;
    private AudioSource musicAS;
    int number;
   
    public enum SoundAction
    {
        Play,
        Pause,
        Stop
    }
    void Awake()
    {
        GameObject go = new GameObject();
        go.name = "Music";
        go.transform.parent = transform;
        musicAS = go.AddComponent<AudioSource>();
        musicAS.volume = 0.5f;
      
        musicAS.loop = false;
        musicAS.playOnAwake = false;
       
    }
    void Start()
    {
        DoMusic(SoundAction.Play);
    }
    void Update()
    {
        if (!musicAS.isPlaying)                    // dont with music
        {  
            musicAS.clip = ChangingMusic(number);
            musicAS.Play();
          
        }
    }
    private AudioClip ChangingMusic(int number)
    {
        return music[Random.Range(0, music.Length)];
    }
    public void DoMusic(SoundAction soundAction)              // pick the alternative case for performing an action 
    {
        switch (soundAction)
        {
            case SoundAction.Play: musicAS.Play(); break;
            case SoundAction.Pause: musicAS.Pause(); break;
            case SoundAction.Stop: musicAS.Stop(); break;
        }
    }
}
