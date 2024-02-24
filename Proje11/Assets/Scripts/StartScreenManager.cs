using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using WebSocketSharp;

public class StartScreenManager : MonoBehaviourPunCallbacks
{
    bool isConnected=false;
    NetworkManager networkManager;
    public GameObject serverScreen, roomScreen;
    public TMP_InputField playerName, roomName;
    public Button buttonCreateRoom, buttonJoinRoom, buttonStartGame;
    public TextMeshProUGUI playerListText,textRoomName;
    void Start()
    {
        networkManager = NetworkManager.instance;
        buttonCreateRoom.interactable = false;
        buttonJoinRoom.interactable = false;
    }
    void Update()
    {
        if(!playerName.text.IsNullOrEmpty() && !roomName.text.IsNullOrEmpty() && isConnected)
        {
            buttonCreateRoom.interactable = true;
            buttonJoinRoom.interactable = true;    
        }
    }
    public override void OnConnectedToMaster()
    {
        isConnected = true;
    }
    public void SetScreen(GameObject screenName)
    {
        serverScreen.SetActive(false);
        roomScreen.SetActive(false);
        screenName.SetActive(true);
    }
    public void OnCreateRoomButton(TMP_InputField roomName)
    {
        networkManager.CreateRoom(roomName.text);
        textRoomName.text = roomName.text;
    }
    public void OnJoinRoomButton(TMP_InputField roomName)
    {
        networkManager.JoinRoom(roomName.text);
        textRoomName.text = roomName.text;
    }
    public void OnPlayerNameUpdate(TMP_InputField playerName)
    {
        PhotonNetwork.NickName = playerName.text;
    }
    public override void OnJoinedRoom()
    {
        SetScreen(roomScreen);
        photonView.RPC("UpdateRoomScreen",RpcTarget.All);
    }
    [PunRPC]
    public void UpdateRoomScreen()
    {
        playerListText.text = "";
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n";
        }

        if (PhotonNetwork.IsMasterClient)
            buttonStartGame.interactable = true;
        else
            buttonStartGame.interactable = false;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateRoomScreen();
    }
    public void OnLeaveRoomButton()
    {
        PhotonNetwork.LeaveRoom();
        SetScreen(serverScreen);
    }
    public void OnStartGameButton()
    {
        networkManager.photonView.RPC("ChangeScene", RpcTarget.All,"GameScene");
        //GameManager.instance.PreGameStart();
    }
}
