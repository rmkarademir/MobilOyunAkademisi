using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameScreenManager : MonoBehaviourPunCallbacks
{
    public static GameScreenManager instance;
    GameManager gameManager;
    public TextMeshProUGUI [] charactersUI = new  TextMeshProUGUI[4];
    public char char1,char2,char3,char4;
    char [] characters = new char[4];
    char [] _playerWordCharacters = new char[4];
    int correctCount=0;
    public GameObject waitMessage,lockPanel,endGamePanel,
                      winGamePanel,loseGamePanel;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gameManager = GameManager.instance;
    }

    void Update()
    {

    }
    public void StartScreen()
    {   
        _playerWordCharacters = gameManager.player2WordCharacters;
        for (int i = 0; i < _playerWordCharacters.Count(); i++)
        {
            charactersUI[i].text = _playerWordCharacters[i].ToString();
        }
        if (!PhotonNetwork.IsMasterClient && !gameManager.player2Turn)
        {
            lockPanel.SetActive(true);
        }
        loseGamePanel.SetActive(false);
        winGamePanel.SetActive(false);
    }
    public void CheckCharacter(GameObject obj)
    {
        var button = obj.GetComponent<Button>();
        var c = button.GetComponentInChildren<TextMeshProUGUI>().text;
        button.interactable = false;
        for (int i = 0; i < _playerWordCharacters.Count(); i++)
        {
            if (_playerWordCharacters[i] == c.ToCharArray()[0])
            {
                correctCount++;
                charactersUI[i].gameObject.SetActive(true);
                var buttonColor = button.colors;
                buttonColor.disabledColor = Color.green;
                button.colors = buttonColor;
            }  
        }
        photonView.RPC("SendChar",RpcTarget.Others,PhotonNetwork.IsMasterClient);
        lockPanel.SetActive(true);
        if(correctCount == _playerWordCharacters.Length)
        {
            winGamePanel.SetActive(true);
            photonView.RPC("LoseGame",RpcTarget.Others);
            //correctCount = 0;
        }
    }
    [PunRPC]
    public void SendChar(bool isPlyer1)
    {
        if (isPlyer1)
        {
            gameManager.player1Turn = false;
            gameManager.player2Turn = true;  
            lockPanel.SetActive(false);
            waitMessage.SetActive(false);
        }
        else if (!isPlyer1)
        {
            gameManager.player1Turn = true;
            gameManager.player2Turn = false;  
            lockPanel.SetActive(false);
            waitMessage.SetActive(false);
        }
    }
    [PunRPC]
    public void LoseGame()
    {
        loseGamePanel.SetActive(true);
        correctCount = 0;
    }
    public void NewGame()
    {
        photonView.RPC("ChangePlayerWantGame",RpcTarget.Others);
        gameManager.isPlayer1WantNewGame = true;
        gameManager.PreGameStart();
    }
    [PunRPC]
    public void ChangePlayerWantGame()
    {
        gameManager.isPlayer2WantNewGame = true;
        gameManager.PreGameStart();
    }
    public void ExitGame()
    {
        photonView.RPC("EndGamePanelActive",RpcTarget.Others);
    }
    [PunRPC]
    public void EndGamePanelActive()
    {
        endGamePanel.SetActive(true);
        photonView.RPC("QuitGame",RpcTarget.Others);
    }
    [PunRPC]
    public void QuitGame()
    {
        Application.Quit();
    }
    //[PunRPC]
    
    /*public void ButtonClick_A()
    {
        
        CheckCharacter("A");
    }
    public void ButtonClick_B()
    {
        CheckCharacter('B');
    }
    public void ButtonClick_C()
    {
        CheckCharacter('C');
    }
    public void ButtonClick_Ç()
    {
        CheckCharacter('Ç');
    }
    public void ButtonClick_D()
    {
        CheckCharacter('D');
    }
    public void ButtonClick_E()
    {
        CheckCharacter('E');
    }
    public void ButtonClick_F()
    {
        CheckCharacter('F');
    }
    public void ButtonClick_G()
    {
        CheckCharacter('G');
    }
    public void ButtonClick_Ğ()
    {
        CheckCharacter('Ğ');
    }
    public void ButtonClick_H()
    {
        CheckCharacter('H');
    }
    public void ButtonClick_İ()
    {
        CheckCharacter('İ');
    }
    public void ButtonClick_I(string a)
    {
        CheckCharacter('I');
    }*/
}
