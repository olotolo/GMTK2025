using System;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour
{




    [SerializeField] Sound[] sounds;
    public float GeneralVolume;

    public static AudioController instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() {
        PlayerPrefs.SetInt("FirstTimePlaying", 1);
        Play("Main");
    }

    public void ChangeGeneralVolume(float volume) {
        GeneralVolume = volume;
        foreach (Sound s in sounds) {

            s.source.volume = GeneralVolume * s.volume;

        }
    }

    public void ChangeVolumeGroup(string groupName, float value) {
        Sound[] group = Array.FindAll(sounds, sound => sound.GroupName == groupName);
        foreach (Sound s in group) {
            s.volume = value;
            s.source.volume = GeneralVolume * value;
        }
    }

    public void ChangeVolume(string name, float value) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("Sound with the Name" + name + "has not been found in AudioController");
            return;
        }
        s.volume = value;
        s.source.volume = GeneralVolume * value;
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("Sound with the Name" + name + "has not been found in AudioController");
            return;
        }
        if (s == null || s.source == null) {
            return;
        }
        if (!s.source.isPlaying) {
            s.source.Play();
        }
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("Sound with the Name" + name + "has not been found in AudioController");
            return;
        }

        s.source.Stop();
    }

    public Sound GetSound(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            // Keep the warning consistent with your Play/Stop methods
            Debug.Log("Sound with the Name " + name + " has not been found in AudioController");
        }
        return s; // Will return null if not found
    }



}
