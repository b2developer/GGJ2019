using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance = null;

    public AudioClip initialMusic = null;
    public AudioClip[] music;

    public AudioSource source1;
    public AudioSource source2;
     
    public float fadeInTime = 2.0f;
    public float fadeTimer = 0.0f;
    public bool fadeSwitch = true;

	// Use this for initialization
	void Awake ()
    {
        instance = this;

        fadeTimer = fadeInTime;

        source1.clip = initialMusic;
        source1.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {

		if (fadeTimer < fadeInTime)
        {
            fadeTimer += Time.deltaTime;

            float scalar = fadeTimer / fadeInTime;

            scalar = Mathf.Clamp01(scalar);

            if (fadeSwitch)
            {
                source1.volume = scalar;
                source2.volume = 1 - scalar;
            }
            else
            {
                source2.volume = scalar;
                source1.volume = 1 - scalar;
            }

            if (fadeTimer > fadeInTime)
            {
                if (fadeSwitch)
                {
                    source2.Stop();
                }
                else
                {
                    source1.Stop();
                }
            }
        }
	}

    public void PlayRandom()
    {
        List<AudioClip> potentialTracks = new List<AudioClip>(music);

        if (fadeSwitch)
        {
            potentialTracks.Remove(source1.clip);
        }
        else
        {
            potentialTracks.Remove(source2.clip);
        }

        AudioClip newMusic = potentialTracks[Mathf.RoundToInt(Random.Range(0.0f, potentialTracks.Count - 1))];

        FadeTo(newMusic);
    }

    public void FadeTo(AudioClip target)
    {
        fadeSwitch = !fadeSwitch;

        fadeTimer = 0.0f;

        if (fadeSwitch)
        {
            source1.clip = target;
            source1.Play();
        }
        else
        {
            source2.clip = target;
            source2.Play();
        }
    }
}
