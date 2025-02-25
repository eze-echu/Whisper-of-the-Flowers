using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager1 : MonoBehaviour
{
    private Dictionary<string, AudioSource> musicSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource> ambientSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource> effectsSources = new Dictionary<string, AudioSource>();



    public void Start()
    {
        if (GameManager.instance.AM == null) GameManager.instance.AM = this;
        else Destroy(gameObject);

        //InitializeAudioSources();
    }

    private void InitializeAudioSources()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            // Asigna cada AudioSource al diccionario correspondiente seg�n su tag
            switch (audioSource.gameObject.tag)
            {
                case "Musica":
                    musicSources.Add(audioSource.gameObject.name, audioSource);
                    break;

                case "Ambiente":
                    ambientSources.Add(audioSource.gameObject.name, audioSource);
                    break;

                case "Efectos":
                    effectsSources.Add(audioSource.gameObject.name, audioSource);
                    break;

                    // Agrega m�s casos seg�n tus necesidades
            }
        }
    }

    public void PlayMusic(AudioSource musicSource)
    {
        Play(musicSource);
    }

    public void PlayAmbient(AudioSource ambientSource)
    {
        Play(ambientSource);
    }

    public void PlayEffect(AudioSource effectSource)
    {
        Play(effectSource);
    }

    private void Play(AudioSource audioSource)
    {
        if (audioSource)
        {
            audioSource.Play();
        }
        else
        {
            //Debug.LogError("Se proporcion� un AudioSource nulo");
        }
    }


    private AudioSource GetAudioSourceWithTag(string tag)
    {
        AudioSource[] audioSourcesWithTag = GameObject.FindGameObjectsWithTag(tag)[0].GetComponents<AudioSource>();
        // Este ejemplo asume que hay solo un objeto con el tag y varios AudioSources en �l.
        // Puedes ajustarlo seg�n tu estructura.
        return audioSourcesWithTag[0];
    }

    /*
    public void FadeOut(string audioName, float customDuration = 3f)
    {
        if (audioSources.TryGetValue(audioName, out AudioSource audioSource))
        {
            StartCoroutine(FadeOutCoroutine(audioSource, customDuration));
        }
        else
        {
            Debug.LogError($"No se encontr� un AudioSource para el nombre {audioName}");
        }
    }
    

    private IEnumerator FadeOutCoroutine(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Restablecer el volumen a su valor original
    }
    */
}
