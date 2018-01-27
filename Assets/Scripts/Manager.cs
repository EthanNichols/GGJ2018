using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject player;

    public List<GameObject> players;
    public float mapSize;

    public bool GameOver = false;

    // Use this for initialization
    void Start()
    {
        CreatePlayers();
        RespawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
        {
            RespawnPlayers();
            GameOver = false;
        }
    }

    private void RespawnPlayers()
    {
        //Temp height of player
        float playerHeight = .5f;

        players[0].transform.position = new Vector3(mapSize / 4, playerHeight, mapSize / 4);
        players[1].transform.position = new Vector3(mapSize / 4 * 3, playerHeight, mapSize / 4);
        players[2].transform.position = new Vector3(mapSize / 4, playerHeight, mapSize / 4 * 3);
        players[3].transform.position = new Vector3(mapSize / 4 * 3, playerHeight, mapSize / 4 * 3);

        foreach(GameObject playerObj in players)
        {
            Debug.Log(playerObj.name);
            playerObj.GetComponent<Player>().ResetCamera();
        }
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
            if (i != 0) { Destroy(cameraObj.GetComponent<AudioListener>()); }

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
