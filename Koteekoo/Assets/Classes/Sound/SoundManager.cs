using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class SoundManager : MonoBehaviour
{

    public AudioClip[] Sounds;
    public AudioClip[] Music;

    public AudioSource _audioSourceTemplate;

    public AudioSource _musicSourceTemplate;

    bool _isOn = true;

    float _startTime;


    // Use this for initialization
    void Start()
    {
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// All sounds will be destroy after 2 sec
    /// </summary>
    /// <param name="index"></param>
    /// <param name="vol"></param>
    /// <param name="random">Will randomize the pitch anbd volume</param>
    /// <returns></returns>
    public AudioSource PlaySound(int index, float vol = 1f, bool random = false, float pitch = 0, bool autoDestroy = true)
    {
        if (Time.time < _startTime + .01f)
        {
            return null;
        }

        //debug
        if (Sounds == null || Sounds.Length == 0)
        {
            return null;
        }

        if (!_isOn)
        {
            return null;
        }

        if (_audioSourceTemplate == null)
        {
            return null;
        }

        var audioSource = Instantiate(_audioSourceTemplate);

        if (audioSource == null)
        {
            return null;
        }

        var clip = Sounds[index];
        audioSource.clip = clip;

        if (random)
        {
            audioSource.pitch = UMath.Random(-3, 3);
            vol = UMath.Random(.4f, 1f);
        }
        if (pitch != 0)
        {
            audioSource.pitch = pitch;
        }
        if (!autoDestroy)
        {
            audioSource.GetComponent<TimedObjectDestructor>().enabled = false;
        }

        audioSource.volume = vol;
        audioSource.Play();

        return audioSource;
    }

    public AudioSource PlaySound(AudioSource source, float vol, float pitch)
    {
        source.pitch = pitch;
        source.volume = vol;
        source.Play();
        return source;
    }


    public void PlayMusic(int index)
    {
        //debug
        if (Music.Length == 0)
        {
            return;
        }

        if (!_isOn)
        {
            return;
        }

        var audioSource = Instantiate(_musicSourceTemplate);

        var clip = Music[index];
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = 0.4f;
        audioSource.Play();
    }
}
