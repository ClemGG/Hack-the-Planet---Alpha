using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    private Sound lastBGMusic;

    public static AudioManager instance;

    // Use this for initialization
    void Awake()
    {
            
        if (instance == null) // càd s'il n'y a pas déjà d'AudioManager dans la scène
        {

            instance = this; // donc on en obtient un

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume * PlayerPrefs.GetFloat("volume", .5f);
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.enabled = false;
                s.source.playOnAwake = false;
            }
            SetupVolume();

        }
        else
        {
            Destroy(gameObject); //Pour éviter les doublons
            return;
        }

        //Pour que la musique ne se répète pas à chaque changement de scène : DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(gameObject);





        //Pour jouer une musique constante à partir de ce script : Play("Nom de la musique"), à cette ligne même.

    }






    public void SetupVolume()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * PlayerPrefs.GetFloat("volume", .5f);
        }
    }







    //Pour jouer un son voulu depuis un autre script : FindObjectOfType<AudioManager>().Play("Nom du son voulu");

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => string.Compare(sound.name, name) == 0);
        

        //print(name);
        //print(string.Compare(s.name, name));  //C'est bien le bon son
        //print(s.source);  
        
        s.source.enabled = true;
        s.source.clip = s.clip;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;



        if (s.isBGMusic)
        {
            StartCoroutine(LerpVolume(s, true));

            if (lastBGMusic != null)
            {
                StartCoroutine(LerpVolume(s, false));
            }
            else
            {
                lastBGMusic = s;
            }

        }
        else
        {
            s.source.volume = s.volume * PlayerPrefs.GetFloat("volume", .5f);
        }



        if (s == null)
        {
            Debug.LogWarning("Son :" + name + " pas trouvé !");
            return;
        }
        s.source.Play();
    }




    private IEnumerator LerpVolume(Sound s, bool monterVolume)
    {
        if (monterVolume)
        {
            while (!Mathf.Approximately(s.source.volume, s.volume * PlayerPrefs.GetFloat("volume", .5f)))
            {
                s.source.volume = Mathf.Lerp(s.source.volume, s.volume * PlayerPrefs.GetFloat("volume", .5f), .1f);
                yield return 0;
            }
        }
        else
        {
            while (!Mathf.Approximately(lastBGMusic.source.volume, 0f))
            {
                lastBGMusic.source.volume = Mathf.Lerp(lastBGMusic.source.volume, 0f, .1f);
                yield return 0;
            }

            Stop(lastBGMusic.name);
            lastBGMusic = s;
        }
    }




    public void Stop(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.enabled = true;

        s.source.Stop();

    }
}