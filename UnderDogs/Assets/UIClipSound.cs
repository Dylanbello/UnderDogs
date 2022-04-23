using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClipSound : MonoBehaviour
{
    AudioSource a_Source;
    public AudioClip UISelected;
    public AudioClip UIOnClicked;

    private void Start() { a_Source = GetComponent<AudioSource>(); }
    public void PlayClip(AudioClip clip) { a_Source.PlayOneShot(clip); }
}
