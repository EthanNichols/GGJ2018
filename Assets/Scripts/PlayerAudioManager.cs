using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {

    private AudioSource AS;

    public AudioClip AC_Dash;
    public AudioClip AC_Block;
    public AudioClip AC_Hit;
    public AudioClip AC_Death;
    public AudioClip AC_Celebrate;

    private int prevPlayed = -1;

    private AudioSource mngrAS;
	// Use this for initialization
	void Start () {
        AS = this.gameObject.GetComponent<AudioSource>();
        mngrAS = FindObjectOfType<Manager>().GetComponent<AudioSource>();
	}
	
    /// <summary>
    /// Play the dash sound centered on this player
    /// </summary>
    public void PlayDash()
    {
        if (prevPlayed == 0)
            return;

        prevPlayed = 0;
        AS.Stop();
        AS.clip = AC_Dash;
        AS.Play();
        Debug.Log("Dash");
    }

    /// <summary>
    /// Play the block sound centered on this player
    /// </summary>
    public void PlayBlock()
    {
        if (prevPlayed == 1)
            return;

        RandomizeValues();
        prevPlayed = 1;
        AS.Stop();
        AS.clip = AC_Block;
        AS.Play();
        Debug.Log("Block");
    }

    /// <summary>
    /// Play the hit sound centered on this player
    /// </summary>
    public void PlayHit()
    {
        if (prevPlayed == 2)
            return;

        RandomizeValues();
        prevPlayed = 2;
        AS.Stop();
        AS.clip = AC_Hit;
        AS.Play();
        Debug.Log("Hit");
    }

    /// <summary>
    /// Play the death sound centered on this player
    /// </summary>
    public void PlayDeath()
    {
        if (prevPlayed == 3)
            return;

        RandomizeValues();
        prevPlayed = 3;
        AS.Stop();
        AS.clip = AC_Death;
        AS.Play();
        Debug.Log("Death");
    }

    /// <summary>
    /// Play the celebrate sound centered on this player
    /// </summary>
    public void PlayCelebrate()
    {
        mngrAS.Stop();
        Invoke("PlayBGM", 7f);
        ResetValues();
        prevPlayed = 4;
        AS.Stop();
        AS.clip = AC_Celebrate;
        AS.Play();
        Debug.Log("Celebrate");
    }

    private void PlayBGM()
    {
        mngrAS.Play();
    }

    private void RandomizeValues()
    {
        AS.pitch = Random.Range(0.8f, 1.2f);
        AS.volume = Random.Range(0.8f, 1.0f);
    }

    private void ResetValues()
    {
        AS.pitch = 1.0f;
        AS.volume = 1.0f;
    }
}
