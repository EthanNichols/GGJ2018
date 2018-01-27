using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public GameObject player;

    public float mapSize;

	// Use this for initialization
	void Start () {
        CreatePlayers();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreatePlayers()
    {
        for (int i=0; i<4; i++)
        {
            GameObject newPlayer = Instantiate(player);

            float playerHeight = .5f;

            switch(i)
            {
                case 0:
                    newPlayer.transform.position = new Vector3(mapSize / 4, playerHeight, mapSize / 4);
                    newPlayer.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0, .5f, .5f, .5f);
                    break;
                case 1:
                    newPlayer.transform.position = new Vector3(mapSize / 4 * 3, playerHeight, mapSize / 4);
                    newPlayer.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(.5f, .5f, .5f, .5f);
                    break;
                case 2:
                    newPlayer.transform.position = new Vector3(mapSize / 4, playerHeight, mapSize / 4 * 3);
                    newPlayer.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(0, 0, .5f, .5f);
                    break;
                case 3:
                    newPlayer.transform.position = new Vector3(mapSize / 4 * 3, playerHeight, mapSize / 4 * 3);
                    newPlayer.transform.Find("Camera").GetComponent<Camera>().rect = new Rect(.5f, 0, .5f, .5f);
                    break;
            }

            newPlayer.transform.Find("Camera").GetComponent<Camera>().rect.Set(0, 0, 0, 0);
        }
    }
}
