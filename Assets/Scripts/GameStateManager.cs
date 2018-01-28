using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour{

    public float restartScreenTimer = 3;
    private float restartReset;

    public string PlayerCount;

    public enum GameState
    {
        mainMenu,
        credits,
        game,
        restart
    }

    [HideInInspector]
    public static GameStateManager dontDestroy = null;

    public GameState currentState;
    public GameState newState;

    private float escapeKeyTimer;

	// Use this for initialization
	void Start () {

        //Only have one instance
        if (dontDestroy == null) { dontDestroy = this; }
        else { Destroy(gameObject); }

        //Set the reset timers
        restartReset = restartScreenTimer;

        //Set the starting game state, and don't destroy the object
        currentState = GameState.mainMenu;
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        //Update the current state
        currentState = newState;

        //Update state functions
        MainMenuEscape();
        RestartScreen();
	}

    private void RestartScreen()
    {
        //Start the timer on the restart screen
        if (currentState == GameState.restart)
        {
            restartScreenTimer -= Time.deltaTime;
        }
        else
        {
            restartScreenTimer = restartReset;
        }

        //Set the new gamestate when the timer is done
        if (restartScreenTimer <= 0)
        {
            newState = GameState.game;
            ConfettiManager.Instance.StopConfetti();
        }
    }

    private void MainMenuEscape()
    {
        //If the escaped key is pressed twice go to the main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escapeKeyTimer > 0)
            {
                newState = GameState.mainMenu;
                SceneManager.LoadScene(0);
            }
            else
            {
                //Reset the escape timer
                escapeKeyTimer = .75f;
            }
        }

        //Run the timer
        escapeKeyTimer -= Time.deltaTime;
    }
}
