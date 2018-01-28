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

	// Use this for initialization
	void Start () {
        AS = this.gameObject.GetComponent<AudioSource>();
	}
	
    /// <summary>
    /// Play the dash sound centered on this player
    /// </summary>
    public void PlayDash()
    {
        AS.Stop();
        AS.clip = AC_Dash;
        AS.Play();
    }

    /// <summary>
    /// Play the block sound centered on this player
    /// </summary>
    public void PlayBlock()
    {
        AS.Stop();
        AS.clip = AC_Block;
        AS.Play();
    }

    /// <summary>
    /// Play the hit sound centered on this player
    /// </summary>
    public void PlayHit()
    {
        AS.Stop();
        AS.clip = AC_Hit;
        AS.Play();
    }

    /// <summary>
    /// Play the death sound centered on this player
    /// </summary>
    public void PlayDeath()
    {
        AS.Stop();
        AS.clip = AC_Death;
        AS.Play();
    }

    /// <summary>
    /// Play the celebrate sound centered on this player
    /// </summary>
    public void PlayCelebrate()
    {
        AS.Stop();
        AS.clip = AC_Celebrate;
        AS.Play();
    }
}
