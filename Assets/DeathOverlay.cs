using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOverlay : MonoBehaviour
{
    public Player player;
    private Renderer r;
    // Use this for initialization
    void Start()
    {
        r = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.dead)
        {
            r.enabled = true;
            gameObject.layer = 0;
        }
        else
        {
            r.enabled = false;
        }
    }
}
