using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerML : Agent
{
    Player playerControls;
    Manager mngr;
    int prevKillCount = 0;
    bool prevIsDead = false;

    public override void InitializeAgent()
    {
        playerControls = GetComponent<Player>();
        mngr = FindObjectOfType<Manager>();
    }

    public override List<float> CollectState()
    {
        List<float> state = new List<float>();
        for (int i = 0; i < 4; i++)
        {
            if (i >= mngr.players.Count)
            {
                for (int j = 0; j < 9; j++)
                    state.Add(0f);

                break;
            }
            Player p = mngr.players[i].GetComponent<Player>();
            if (p.dead)
                state.Add(0);
            else state.Add(1);

            state.Add((p.transform.position.x - -3.8f) / (13.55f - -3.8f));
            state.Add((p.transform.position.z - -3.81f) / (13.75f - -3.81f));
            state.Add(p.transform.rotation.eulerAngles.y / 180.0f - 1.0f);

            if (p.IsBlocking)
                state.Add(0);
            else state.Add(1);

            if (p.IsPunching)
                state.Add(0);
            else state.Add(1);
        }
        return state;
    }

    public override void AgentStep(float[] act)
    {
        int currKillCount = playerControls.kills;
        if (currKillCount > prevKillCount)
            reward += 1.0f;

        bool currIsDead = playerControls.dead;
        if (currIsDead != prevIsDead && currIsDead)
            reward += -1.0f;

        switch ((int)act[0])
        {
            case 0:
                playerControls.StartBlock();
                break;
            case 1:
                playerControls.StartPunch();
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

        prevIsDead = currIsDead;
        prevKillCount = currKillCount;
    }

    public override void AgentReset()
    {

    }
}
