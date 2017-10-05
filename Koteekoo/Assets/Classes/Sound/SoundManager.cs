using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void PlaySound(int index, float vol = 1f, bool random = false)
    {
        if (Time.time < _startTime + 1)
        {
            return;
        }

        //debug
        if (Sounds == null || Sounds.Length == 0)
        {
            return;
        }

        if (!_isOn)
        {
            return;
        }

        if (_audioSourceTemplate == null)
        {
            return;
        }

        var audioSource = Instantiate(_audioSourceTemplate);

        if (audioSource == null)
        {
            return;
        }

        var clip = Sounds[index];
        audioSource.clip = clip;

        if (random)
        {
            audioSource.pitch = UMath.Random(-3, 3);
            vol = UMath.Random(.4f, 1f);
        }

        audioSource.volume = vol;
        audioSource.Play();
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
