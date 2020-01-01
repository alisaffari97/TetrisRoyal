using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickStartButton;
    [SerializeField]
    private GameObject quickCancelButton;
    [SerializeField]
    private GameObject FindingOpponent;
    [SerializeField]
    private GameObject onePlayer;
    [SerializeField]
    private GameObject twoPlayer;
    [SerializeField]
    private GameObject BackHome;

    private int Size;

    public int RoomSize(int value)
    {
        int size = 0;
        value = size;
        return size;
    }
    public override void OnConnectedToMaster() //Callback function for when the first connection is established successfully.
    {
        PhotonNetwork.AutomaticallySyncScene = true; //Makes it so whatever scene the master client has loaded is the scene all other clients will load
        quickStartButton.SetActive(true);
    }
    public void QuickStart() //Paired to the Quick Start button
    {
        quickStartButton.SetActive(false);
        onePlayer.SetActive(true);
        twoPlayer.SetActive(true);
        BackHome.SetActive(true);
    }
    public void Mode1()
    {
        Size = 2;
        quickCancelButton.SetActive(true);
        FindingOpponent.SetActive(true);
        onePlayer.SetActive(false);
        twoPlayer.SetActive(false);
        BackHome.SetActive(false);
        RoomSize(2);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Mode 1 Selected");
    }
    public void Mode2()
    {
        Size = 4;
        quickCancelButton.SetActive(true);
        FindingOpponent.SetActive(true);
        onePlayer.SetActive(false);
        twoPlayer.SetActive(false);
        BackHome.SetActive(false);
        RoomSize(4);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Mode 2 Selected");
    }
    public void BackMain()
    {
        quickCancelButton.SetActive(false);
        FindingOpponent.SetActive(false);
        quickStartButton.SetActive(true);
        onePlayer.SetActive(false);
        twoPlayer.SetActive(false);
        BackHome.SetActive(false);
    }
    public override void OnJoinRandomFailed(short returnCode, string message) //Callback function for if we fail to join a rooom
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }
    void CreateRoom() //trying to create our own room
    {
        Debug.Log("Creating room now");
        int randomRoomNumber = Random.Range(0, 10000); //creating a random name for the room
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)Size };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps); //attempting to create a new room
        Debug.Log(randomRoomNumber);
    }
    public override void OnCreateRoomFailed(short returnCode, string message) //callback function for if we fail to create a room. Most likely fail because room name was taken.
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom(); //Retrying to create a new room with a different name.
    }
    public void QuickCancel() //Paired to the cancel button. Used to stop looking for a room to join.
    {
        quickCancelButton.SetActive(false);
        FindingOpponent.SetActive(false);
        onePlayer.SetActive(true);
        twoPlayer.SetActive(true);
        BackHome.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}