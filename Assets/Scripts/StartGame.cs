using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    GameStateManager mngr;

    public static List<bool> players = new List<bool>();

    // Use this for initialization
    void Start()
    {
        players.Add(false);
        players.Add(false);
        players.Add(false);
        players.Add(false);

        mngr = FindObjectOfType<GameStateManager>();
    }

    public void SelectComputers()
    {
        transform.parent.parent.Find("PlayerSelect").gameObject.SetActive(true);
    }

    public void SetPlayerNum(int num)
    {
        //Debug.Log(num);

        if (!players[num]) {
            gameObject.GetComponent<Image>().color = new Color(.2f, .8f, .2f);
            players[num] = true;
        }
        else {
            players[num] = false;
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }

    public void StartTheGame()
    {
        string playing = "";

        for(int i=0; i<4; i++) {

            //Debug.Log(i);

            if (players[i])
            {
                playing += '1';
            } else
            {
                playing += '0';
            }
        }

        //Debug.Log(playing);

        GameObject.FindGameObjectWithTag("StateManager").GetComponent<GameStateManager>().PlayerCount = playing;

        if (mngr)
            mngr.newState = GameStateManager.GameState.game;
        SceneManager.LoadScene(1);
    }

}
