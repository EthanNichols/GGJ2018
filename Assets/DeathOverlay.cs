using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOverlay : MonoBehaviour
{
    public Player player;
    public Sprite robotImg;
    public Sprite skullImg;
    private SpriteRenderer r;
    // Use this for initialization
    void Start()
    {
        r = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.dead)
        {
            r.enabled = true;
            gameObject.layer = 0;
            r.sprite = skullImg;
        }
        else if (player.isML)
        {
            r.enabled = true;
            gameObject.layer = 0;
            r.sprite = robotImg;
        }
        else r.enabled = false;
    }
}
