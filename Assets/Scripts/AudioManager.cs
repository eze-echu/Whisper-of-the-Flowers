using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    public List<Sound> clips;
    public static AudioManager Instance (){
        if(!audioManager){
            audioManager = FindObjectOfType<AudioManager>();
            if(!audioManager){
                Debug.LogError("No hay AudioManager activo");
            }
        }
        return audioManager;
    }

    private void Awake() {
        /*if (instance){
            Destroy(gameObject);
        }*/
        
        audioManager = this;
        
        foreach(var s in clips){
            AddSources(s, 0);
        }
        if(audioManager.gameObject.name.Contains("BGM")){
            Debug.Log("a");
            _CheckIfCurrentlyPlayingBGM();
        }
    }
    public void Play(string name){
        //Sound s = clips.Find(clips, Sound => Sound.Name == name);
        Sound s = new Sound();
        foreach (var song in clips){
            if(song.Name == name){
                s = song;
            }
        }
        if(s != null){
            s.source.Play();
            Debug.Log("Playing: " + name);
        }
        else{
            Debug.LogError("Missing sound: " + name);
        }
    }
    public void Play(string name, GameObject sourceGO){
        //Sound s = clips.Find(clips, Sound => Sound.Name == name);
        Sound s = new Sound();
        foreach (var song in clips){
            if(song.Name == name && song.source.gameObject == sourceGO){
                s = song;
            }
        }
        if(s != null){
            //Debug.Log("Playing: " + name);
            s.source.Play();
        }
        else{
            Debug.LogError("Missing sound: " + name);
        }
    }
    public void Stop(string name){
        //Sound s = Array.Find(clips, Sound => Sound.Name == name);
        Sound s = new Sound();
        foreach (var song in clips){
            if(song.Name == name){
                s = song;
            }
        }
        if(s != null){
            s.source.Stop();
        }
        else{
            Debug.LogError("Missing sound: " + name);
        }
    }
    public void Stop(string name, GameObject sourceGO){
        //Sound s = Array.Find(clips, Sound => Sound.Name == name);
        Sound s = new Sound();
        foreach (var song in clips){
            if(song.Name == name && song.source.gameObject == sourceGO){
                s = song;
            }
        }
        if(s != null){
            s.source.Stop();
        }
        else{
            Debug.LogError("Missing sound: " + name);
        }
    }
    private void _CheckIfCurrentlyPlayingBGM(){
        AudioManager[] s = FindObjectsOfType<AudioManager>();
        //Debug.Log(s.Length);
        if(s.Length > 1){
            foreach(var a in s){
                if (a.name.Contains("BGM") && a.gameObject != this.gameObject) {
                    Debug.Log("Destroy");
                    Destroy(a.gameObject);
                }
                else if (a.name.Contains("BGM"))
                {
                    DontDestroyOnLoad(a.gameObject);
                }
            }
        }
    }
    public Sound FindBySource(AudioSource audioSource){
        Sound s = new Sound();
        foreach (var song in clips){
            if(song.source == audioSource){
                s = song;
            }
        }
        return s;
    }
    public int FindIDBySource(AudioSource audioSource){
        int s = new int();
        int i = 0;
        foreach (var song in clips){
            if(song.source == audioSource){
                s = i;
            }
            i++;
        }
        return s;
    }
    public void AddSources(Sound s, int spatial){
        if (s.source == null){
            s.source = gameObject.AddComponent<AudioSource>();
        }
        else if (s.source.clip != null){
            s.source = s.source.gameObject.AddComponent<AudioSource>();
        }
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.playOnAwake = s.playOnAwake;
        s.source.spatialBlend = spatial;
        if(s.playOnAwake && s.source.enabled){
            s.source.Play();
            if(s.keepPlayingAfterLoad){
                DontDestroyOnLoad(s.source.gameObject);
            }
        }
    }

    public static void SwitchNoise(float volume)
    {
        AudioListener.volume = volume;
        print(AudioListener.volume);
    }
}
