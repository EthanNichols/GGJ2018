using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerML : Agent
{
    Player playerControls;
    Manager mngr;

    [SerializeField]
    LayerMask mask;

    private float timer = 0f;
    private Vector3 startPos;
    private Quaternion startRot;

    public override void InitializeAgent()
    {
        playerControls = GetComponent<Player>();
        mngr = FindObjectOfType<Manager>();
        int num = (1 << 8) | (1 << 9) | (1 << 10) | (1 << 11);
        mask = num;
        StartCoroutine("Raycast");

        startPos = new Vector3(3f, 0.3f, 3f);
        startRot = Quaternion.identity;
    }

    public override List<float> CollectState()
    {
        List<float> state = new List<float>();
        state.Add((transform.position.x - -3.8f) / (13.55f - -3.8f));
        state.Add((transform.position.z - -3.81f) / (13.75f - -3.81f));
        state.Add(transform.rotation.eulerAngles.y / 180.0f - 1.0f);
        //for (int i = 0; i < 4; i++)
        //{
        //    if (i >= mngr.players.Count)
        //    {
        //        for (int j = 0; j < 6; j++)
        //            state.Add(0f);

        //        break;
        //    }
        //    Player p = mngr.players[i].GetComponent<Player>();
        //    if (p.dead)
        //        state.Add(0);
        //    else state.Add(1);

        //    state.Add((p.transform.position.x - -3.8f) / (13.55f - -3.8f));
        //    state.Add((p.transform.position.z - -3.81f) / (13.75f - -3.81f));
        //    state.Add(p.transform.rotation.eulerAngles.y / 180.0f - 1.0f);

        //    if (p.IsBlocking)
        //        state.Add(0);
        //    else state.Add(1);

        //    if (p.IsPunching)
        //        state.Add(0);
        //    else state.Add(1);
        //}

        state.Add(playerControls.RecoveryTime);
        return state;
    }

    public override void AgentStep(float[] act)
    {
        timer += Time.deltaTime;
        if (timer > 5f)
        {
           // done = true;
        }
        bool currIsDead = playerControls.dead;

        if ((transform.position.x > -2f && transform.position.x < 12f) &&
            (transform.position.z > -2f && transform.position.z < 12f))
        {
            reward += 0.2f;
        }
        else
        {
            reward -= 1.0f;
            //done = true;
        }
        switch ((int)act[0])
        {
            case 0:
                //if (playerControls.RecoveryTime > -Time.deltaTime)
                    //reward += 0.01f;
                playerControls.StartBlock();
                break;
            case 1:
                playerControls.StartPunch();
                if (!currIsDead)
                {
                    if ((transform.position.x > 0f && transform.position.x < 10f) &&
                        (transform.position.z > 0f && transform.position.z < 10f))
                    {
                        reward += 0.4f;
                    }
                }

                break;
            case 2:
                playerControls.RotateLeft();
                break;
            case 3:
                playerControls.RotateRight();
                break;
            default:
                break;
        }
    }

    IEnumerator Raycast()
    {
        for (; ;)
        {
            if (Physics.Raycast(transform.position + Vector3.up, transform.forward, float.MaxValue, mask))
            {
                //reward += 0.2f;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public override void AgentReset()
    {
        timer = 0f;
        //playerControls.Respawn();
        //transform.position = startPos;
        //transform.rotation = startRot;
    }
}
