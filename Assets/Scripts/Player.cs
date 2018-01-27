using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Rotation and punch speed
    public float rotationSpeed = 50f;
    public float punchForce = 10f;

    //Timers
    public float animationTime = .4f;
    public float recoverTime = .2f;
    private float animationReset;
    public float recoverReset;

    //Input keys
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode punchKey;
    public KeyCode blockKey;


    private float rotationAmount = 0;

    //Whether the player is punching ornot
    private bool punching = false;
    private bool blocking = false;

	// Use this for initialization
	void Start () {
        //Set the reset timers
        animationReset = animationTime;
        recoverReset = recoverTime;
	}
	
	// Update is called once per frame
	void Update () {
        //Test for inputes
        InputCommands();
	}

    private void InputCommands()
    {
        //Get rotation inpute
        if (Input.GetKey(leftKey)) { RotateLeft(); }
        if (Input.GetKey(rightKey)) { RotateRight(); }

        if (animationTime <= 0 && recoverTime <= 0)
        {
            //Make the player punch
            if (Input.GetKeyDown(punchKey)) {
                punching = true;

                //Reset the timers
                animationTime = animationReset;
                recoverTime = recoverReset;
            }

            //Make the player block
            if (Input.GetKeyDown(blockKey)) {
                blocking = true;

                //Reset the timers
                animationTime = animationReset;
                recoverTime = recoverReset;
            }

        //Start the recover timer
        } else if (animationTime <= 0)
        {
            recoverTime -= Time.deltaTime;

            //Set the the player to not do anything and to not move
            punching = false;
            blocking = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            //Try to run the two player actions
            Punch();
            Block();

            //Start the animation timer, and reset the recover timer
            animationTime -= Time.deltaTime;
            recoverTime = recoverReset;
        }
    }

    public void RotateLeft()
    {
        //Calc and set the rotation
        rotationAmount -= rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, rotationAmount, 0);
    }

    public void RotateRight()
    {
        //Calc and set the rotation
        rotationAmount += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, rotationAmount, 0);
    }

    public void Punch()
    {
        //Make sure the player isn't blocking and is punching
        if (blocking || !punching) { return; }

        GetComponent<Rigidbody>().velocity = transform.forward * punchForce;
    }

    public void Block()
    {
        //Make sure the player isn't punching and is blocking
        if (punching || !blocking) { return; }

        if (animationTime > 0 && blocking)
        {

        }
    }
}
