using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public AudioClip intro;
    public AudioClip loop;

    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    {
        audio.loop = false;
        audio.clip = intro;
        audio.Play();

        float introLength = intro.length;
        yield return new WaitForSeconds(introLength);

        audio.loop = true;
        audio.clip = loop;
        audio.Play();
    }
}
