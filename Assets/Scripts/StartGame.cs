using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    GameStateManager mngr;

    // Use this for initialization
    void Start()
    {
        mngr = FindObjectOfType<GameStateManager>();
    }

    public void SelectComputers()
    {
        transform.parent.parent.Find("PlayerSelect").gameObject.SetActive(true);
    }

    public void StartTheGame(int playerCount)
    {
        GameObject.FindGameObjectWithTag("StateManager").GetComponent<GameStateManager>().PlayerCount = playerCount;

        if (mngr)
            mngr.newState = GameStateManager.GameState.game;
        SceneManager.LoadScene(1);
    }

}
