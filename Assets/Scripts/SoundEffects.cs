using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public List<AudioClip> soundEffects = new List<AudioClip>();

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    AudioSource audioSource;

    public static SoundEffects GetInstance()
    {
        return GameObject.FindGameObjectWithTag("chimera").GetComponent<SoundEffects>();
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (AudioClip se in soundEffects)
        {
            audioClips[se.name] = se;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(string name, float volume = 1f)
    {
        audioSource.PlayOneShot(audioClips[name], volume);
    }
}
