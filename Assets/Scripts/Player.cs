﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Rotation and punch speed
    public float rotationSpeed = 50f;
    public float punchForce = 10f;

    //Timers
    public float animationTime = .4f;
    public float recoverTime = .2f;
    private float animationReset;
    private float recoverReset;

    [HideInInspector]
    public int wins;
    public int kills;

    public int playerNum;
    public bool dead = false;
    public float rotationAmount = 0;

    private GameObject cameraObj;
    private Vector3 cameraDefLocalPos;
    private List<GameObject> extraCameras = new List<GameObject>();
    private PlayerAudioManager PAM;

    //Whether the player is punching ornot
    [SerializeField]
    private bool punching = false;

    [SerializeField]
    private bool blocking = false;

    public GameObject[] bookPrefabs;
    public Transform[] wrists;

    private bool initBooks = false;

    private Renderer[] renderers;
    //Animations
    Animator animator;
    DeathParticleManager deathParticle;
    BlockParticleManager blockParticle;

    public bool isML = false;

    public bool IsBlocking { get { return blocking; } }
    public bool IsPunching { get { return punching; } }
    public float RecoveryTime { get { return recoverTime; } }

    // Use this for initialization
    void Awake()
    {
        //Set the reset timers
        animationReset = animationTime;
        recoverReset = recoverTime;
        animator = GetComponent<Animator>();

        cameraObj = transform.Find("Camera").gameObject;
        cameraDefLocalPos = cameraObj.transform.localPosition;

        renderers = GetComponentsInChildren<Renderer>();
        deathParticle = GetComponentInChildren<DeathParticleManager>();
        blockParticle = GetComponentInChildren<BlockParticleManager>();
        PAM = this.gameObject.GetComponent<PlayerAudioManager>();

    }

    public void InitML()
    {
        Brain b = FindObjectOfType<Brain>();
        if (b)
        {
            PlayerML agent = gameObject.AddComponent<PlayerML>();
            agent.GiveBrain(b);
            isML = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dead || GameObject.Find("Manager").GetComponent<Manager>().gameOver) {
            //PAM.PlayDeath();
            transform.LookAt(new Vector3(5, transform.position.y, 5));
            animator.SetInteger("State", 0); //Resets player to idle

            return;
        }

        //Test for inputes
        InputCommands();
    }

    public void ResetCamera(GameObject obj, int index)
    {
        if (obj == null)
        {
            obj = gameObject;
            extraCameras.Clear();
        }
        if (cameraObj == null) { return; }

        //foreach (GameObject extraCam in extraCameras)
        //{
        //    extraCam.transform.SetParent(obj.transform);
        //    extraCam.transform.localPosition = Vector3.zero;
        //    extraCam.transform.localRotation = Quaternion.identity;
        //}

        cameraObj.transform.SetParent(transform);
        cameraObj.transform.localPosition = cameraDefLocalPos;
        cameraObj.transform.localRotation = Quaternion.identity;

        //obj.GetComponent<Player>().extraCameras = extraCameras;
        //obj.GetComponent<Player>().extraCameras.Add(cameraObj);

        //Debug.Log(index);
        if (index >= 0)
        {
            gameObject.layer = 8 + index;
            foreach (Renderer r in renderers)
                r.gameObject.layer = 8 + index;
            int layer = 1 << (8 + index);
            cameraObj.GetComponent<Camera>().cullingMask &= ~layer;
        }
        else
        {
            cameraObj.transform.parent = transform;
            cameraObj.transform.position = new Vector3(0f, -100f * gameObject.layer, 0f);
        }

    }
    public void Respawn()
    {
        dead = false;
        punching = false;
        blocking = false;

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        animationTime = 0;
        recoverTime = 0;
    }

    public void InitBook(int index)
    {
        if (initBooks) return;
        if (index < 0) index = 0;
        else if (index >= bookPrefabs.Length) index %= bookPrefabs.Length;

        for (int i = 0; i < wrists.Length; i++)
        {
            GameObject obj = Instantiate(bookPrefabs[index], wrists[i]);
            obj.layer = 0;
        }
    }

    private void InputCommands()
    {
        //Get rotation inpute
        if (Input.GetAxis("Player" + playerNum + "Move") < 0) { RotateLeft(); }
        else if (Input.GetAxis("Player" + playerNum + "Move") > 0) { RotateRight(); }

        if (animationTime <= 0 && recoverTime <= 0)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            animator.SetInteger("State", 0); //Resets player to idle

            //Make the player punch
            if (Input.GetAxis("Player" + playerNum + "Action") < 0) { StartPunch(); }
            //Make the player block
            if (Input.GetAxis("Player" + playerNum + "Action") > 0) { StartBlock(); }

            //Start the recover timer
        }
        else if (animationTime <= 0)
        {
            recoverTime -= Time.deltaTime;

            //Set the the player to not do anything and to not move
            punching = false;
            blocking = false;
            animator.SetInteger("State", 0); //Resets player to idle

            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            //Try to run the two player actions
            //Punch();
            //Block();

            //Start the animation timer, and reset the recover timer
            animationTime -= Time.deltaTime;
            //recoverTime = recoverReset;
        }
    }

    /// <summary>
    /// Rotate the object left and right
    /// </summary>
    public void RotateLeft()
    {
        if (dead || GameObject.Find("Manager").GetComponent<Manager>().gameOver)
        {
            transform.LookAt(new Vector3(5, transform.position.y, 5));
            return;
        }
        //Calc and set the rotation
        rotationAmount -= rotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
    }
    public void RotateRight()
    {
        if (dead || GameObject.Find("Manager").GetComponent<Manager>().gameOver)
        {
            transform.LookAt(new Vector3(5, transform.position.y, 5));
            return;
        }
        //Calc and set the rotation
        rotationAmount += rotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    /// <summary>
    /// Start functions for blocking and punching
    /// </summary>
    public void StartPunch()
    {
        if (dead || GameObject.Find("Manager").GetComponent<Manager>().gameOver)
        {
            transform.LookAt(new Vector3(5, transform.position.y, 5));
            return;
        }
        if (animationTime > 0 || recoverTime > 0)
            return;
        punching = true;

        //Reset the timers
        animationTime = animationReset;
        recoverTime = recoverReset;

        GetComponent<Rigidbody>().velocity = transform.forward * punchForce;
        animator.SetInteger("State", 1);

        PAM.PlayDash();

    }
    private void Punch()
    {

        //Make sure the player isn't blocking and is punching
        if (blocking || !punching) { return; }

        GetComponent<Rigidbody>().velocity = transform.forward * punchForce;
        animator.SetInteger("State", 1);
        
    }
    public void StartBlock()
    {
        if (dead || GameObject.Find("Manager").GetComponent<Manager>().gameOver)
        {
            transform.LookAt(new Vector3(5, transform.position.y, 5));
            return;
        }
        if (animationTime > 0 || recoverTime > 0)
            return;
        blocking = true;

        //Reset the timers
        animator.SetInteger("State", 2);
        animationTime = animationReset * 1.75f;
        recoverTime = recoverReset * .8f;
    }
    private void Block()
    {
        //Make sure the player isn't punching and is blocking
        if (punching || !blocking) { return; }

        if (animationTime > 0 && blocking)
        {

        }
    }

    private void OnCollisionStay(Collision col)
    {
        //If a player doesn't hit you ignore it
        if (!col.gameObject.name.Contains("Player")) { return; }

        Player otherPlayer = col.gameObject.GetComponent<Player>();
        //If the player that hit you is punching teleport you down
        if (otherPlayer.punching)
        {
            PlayerML ml = GetComponent<PlayerML>();
            if (blocking)
            {
                if (ml)
                {
                   // ml.reward += 0.8f;
                }

                otherPlayer.recoverTime = recoverReset * 3;
                otherPlayer.animationTime = 0;
                otherPlayer.punching = false;

                GetComponent<Rigidbody>().velocity = Vector3.zero;
                PAM.PlayBlock();
                blockParticle.PlayParticle();
            }
            else
            {
                if (dead)
                    return;

                deathParticle.PlayParticles();
                if (ml)
                {
                    //ml.reward -= 0.8f;
                }


                //Increase the amount of kills the player has
                col.gameObject.GetComponent<Player>().kills++;

                transform.position += Vector3.down * 10;
                ResetCamera(col.gameObject, -1);
                dead = true;
                otherPlayer.PAM.PlayHit();
                PAM.PlayDeath();
            }
        }
    }
}
