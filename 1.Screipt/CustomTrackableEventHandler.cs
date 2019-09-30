using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler
{
    public enum AudioClipName { foundSound = 0, lostSound = 1 }

    public AudioSource tackableAudioSource;

    public AudioClip[] AudioSounds;

    public Animator animator;

    private void Awake()
    {
        transform.GetComponent<CustomCardDB>().enabled = false;
    }

    public void SoundPlay(int num)
    {
        tackableAudioSource.PlayOneShot(AudioSounds[num]);
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        gameObject.GetComponent<CustomCardDB>().enabled = true;

        SoundPlay((int)AudioClipName.foundSound);

        //animator.Play(0);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        gameObject.GetComponent<CustomCardDB>().enabled = false;

        SoundPlay((int)AudioClipName.lostSound);
    }
}