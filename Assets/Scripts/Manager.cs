using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int winsNeeded = 3;
    public GameObject player;

    public float mapSize;

    [HideInInspector]
    public List<GameObject> players;

    public bool gameOver = false;

    private GameObject winner;
    private GameStateManager stateManager;

    // Use this for initialization
    void Start()
    {
        stateManager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<GameStateManager>();

        CreatePlayers();
        RespawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameOver();

        //If the game is over and the state isn't the restart screen, set it to restart
        if (gameOver && stateManager.currentState != GameStateManager.GameState.restart)
        {
            stateManager.newState = GameStateManager.GameState.restart;
        }

        //If the new state is the game, and the game is over, respawn players and state a new round
        if (gameOver && stateManager.newState == GameStateManager.GameState.game)
        {
            //Increase the amount of wins the player has
            if (winner)
            {
                winner.GetComponent<Player>().wins++;

                //If the winning player won enough, end the game
                if (winner.GetComponent<Player>().wins >= winsNeeded)
                {
                    stateManager.newState = GameStateManager.GameState.mainMenu;
                }
            }

            RespawnPlayers();
            gameOver = false;
        }
    }

    private void CheckGameOver()
    {
        if (gameOver) return;

        //The amount of players alive
        int aliveCount = 0;
        winner = null;

        //Add each player that is alive to the count
        foreach (GameObject playerObj in players)
        {
            if (!playerObj.GetComponent<Player>().dead)
            {
                aliveCount++;
                winner = playerObj;
            }
        }

        //If there is one or less players left end the game
        if (aliveCount <= 1)
        {
            gameOver = true;

            if (winner)
            {
                PlayerML winningAgent = winner.GetComponent<PlayerML>();
                if (winningAgent)
                    winningAgent.reward += 1.0f;
            }

            foreach (GameObject playerObj in players)
            {
                PlayerML agent = playerObj.GetComponent<PlayerML>();
                if (!agent) return;
                else agent.done = true;
            }
        }
    }

    private void RespawnPlayers()
    {
        //Reset the player rotation and their camera
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.localRotation = Quaternion.identity;
            Player p = players[i].GetComponent<Player>();

            p.ResetCamera(null, i);
            p.Respawn();
            p.InitBook(i);
        }

        //Temp height of player
        float playerHeight = -0.2756758f;

        //Set the player's spawn positions
        players[0].transform.position = new Vector3(mapSize / 5, playerHeight, mapSize / 5);
        players[1].transform.position = new Vector3(mapSize / 5 * 4, playerHeight, mapSize / 5);
        players[2].transform.position = new Vector3(mapSize / 5, playerHeight, mapSize / 5 * 4);
        players[3].transform.position = new Vector3(mapSize / 5 * 4, playerHeight, mapSize / 5 * 4);
    }

    private void CreatePlayers()
    {
        for (int i = 0; i < 4; i++)
        {
            //Create a and set information about the new player
            GameObject newPlayer = Instantiate(player);
            newPlayer.GetComponent<Player>().playerNum = (i + 1);
            newPlayer.name = "Player " + (i + 1);

            players.Add(newPlayer);

            //Get the camera on the player
            GameObject cameraObj = newPlayer.transform.Find("Camera").gameObject;

            //Make sure there is only one audio listener
            //if (i != 0) { Destroy(cameraObj.GetComponent<AudioListener>()); }

            //Set the position for the player, and where the camera is viewed
            switch (i)
            {
                case 0:
                    cameraObj.GetComponent<Camera>().rect = new Rect(0, .5f, .5f, .5f);
                    break;
                case 1:
                    cameraObj.GetComponent<Camera>().rect = new Rect(.5f, .5f, .5f, .5f);
                    break;
                case 2:
                    cameraObj.GetComponent<Camera>().rect = new Rect(0, 0, .5f, .5f);
                    break;
                case 3:
                    cameraObj.GetComponent<Camera>().rect = new Rect(.5f, 0, .5f, .5f);
                    break;
            }
        }
    }
}
