using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    GameScreenManager gameScreenManager;
    public GameObject startScreen,preGameScreen,gameScreen;
    public char [] player1WordCharacters = new char[4],
                   player2WordCharacters = new char[4];
    public bool isPlayer1Ready = false,
                isPlayer2Ready = false,
                player1Turn = true,
                player2Turn = false,
                isPlayer1WantNewGame = false,
                isPlayer2WantNewGame = false;
    public int deneme=10;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gameScreenManager = GameScreenManager.instance;
    }

    void Update()
    {
        
    }
    public void PreGameStart()
    {
        if(isPlayer1WantNewGame && isPlayer2WantNewGame)
        {
            PhotonNetwork.LoadLevel("GameScene");
            // startScreen.SetActive(false);
            // preGameScreen.SetActive(true);
            // gameScreen.SetActive(false);
        }
    }
    public void GameStart()
    {
        if(isPlayer1Ready && isPlayer2Ready)
        { 
            preGameScreen.SetActive(false);
            gameScreen.SetActive(true);
            gameScreenManager.StartScreen();
        }
    }
}
