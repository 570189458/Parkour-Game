using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip button;
    public AudioClip coin;
    public AudioClip getitems;
    public AudioClip hit;
    public AudioClip slide;

    public static AudioManager _instance;

    public Sprite SoundOn;
    public Sprite SoundOff;

    public Image soundImage;

    private void PlayAudio(AudioClip clip)
    {
        if(GameAttribute._instance.SoundOn)
            AudioSource.PlayClipAtPoint(clip, PlayerControl._instance.transform.position);
    }

    public void SwitchSound()
    {
        GameAttribute._instance.SoundOn = !GameAttribute._instance.SoundOn;
        soundImage.sprite = GameAttribute._instance.SoundOn ? SoundOn : SoundOff;
    }

    public void PlayButtonAudio()
    {
        PlayAudio(button);
    }

    public void PlayCoinAudio()
    {
        PlayAudio(coin);
    }

    public void PlayGetItemsAudio()
    {
        PlayAudio(getitems);
    }

    public void PlayHitAudio()
    {
        PlayAudio(hit);
    }

    public void PlaySlideAudio()
    {
        PlayAudio(slide);
    }
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
