using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    #region Variables
    [SerializeField]
    private GameObject quickCancelButton;
    [SerializeField]
    private GameObject FindingOpponent;
    [SerializeField]
    private GameObject onePlayer;
    [SerializeField]
    private GameObject twoPlayer;
    [SerializeField]
    private GameObject quiBtn;
    [SerializeField]
    private GameObject okBtn;
    [SerializeField]
    private GameObject timeoutText;
    [SerializeField]
    public Text playerConnected;

    private int Size;
    private bool enteredRoom = false;
    private bool insideRoom = false;

    private string userId;
    #endregion

    private void Awake()
    {
        userId = "" + Random.Range(-1000000, 1000000);
    }

    private void Start()
    {
        
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.AuthValues = new AuthenticationValues(userId);
            Debug.Log("Connecting to Server...");
            PhotonNetwork.ConnectUsingSettings();  
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
    }

    private void Update()
    {
        Debug.Log(PhotonNetwork.CurrentRoom);
        if (insideRoom)
        {
            timeoutText.SetActive(true);
            okBtn.SetActive(true);
            FindingOpponent.SetActive(false);
            quickCancelButton.SetActive(false);
        }

        if (PhotonNetwork.IsConnected)
        {
            playerConnected.text = "Players Connected: " + PhotonNetwork.CountOfPlayers.ToString();
        }
        else
        {
            playerConnected.text = "You're Offline";
        }
        
    }   

    public IEnumerator timeOut()
    {
        yield return new WaitForSeconds(15);
        insideRoom = true; 
    }

    public int RoomSize(int value)
    {
        int size = 0;
        value = size;
        return size;
    }

    public void Mode1()
    {
        Size = 2;
        quickCancelButton.SetActive(true);
        FindingOpponent.SetActive(true);
        onePlayer.SetActive(false);
        twoPlayer.SetActive(false);
        quiBtn.SetActive(false);
        RoomSize(2);
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public void Mode2()
    {
        Size = 4;
        quickCancelButton.SetActive(true);
        FindingOpponent.SetActive(true);
        onePlayer.SetActive(false);
        twoPlayer.SetActive(false);
        quiBtn.SetActive(false);
        RoomSize(4);
        PhotonNetwork.JoinRandomRoom();

    }

    public void BackMain()
    {
        Application.Quit();
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
        enteredRoom = true;
        StartCoroutine(timeOut());        
    }

    public override void OnCreateRoomFailed(short returnCode, string message) //callback function for if we fail to create a room. Most likely fail because room name was taken.
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom(); //Retrying to create a new room with a different name.
    }

    public void QuickCancel() //Paired to the cancel button. Used to stop looking for a room to join.
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }        
        quickCancelButton.SetActive(false);
        FindingOpponent.SetActive(false);
        onePlayer.SetActive(true);
        twoPlayer.SetActive(true);
        quiBtn.SetActive(true);
        StopAllCoroutines();
        insideRoom = false;
        Debug.Log("Left Room");
    }

    public void OkCancel()
    {
        insideRoom = false;
        quickCancelButton.SetActive(false);
        FindingOpponent.SetActive(false);
        onePlayer.SetActive(true);
        twoPlayer.SetActive(true);
        quiBtn.SetActive(true);
        timeoutText.SetActive(false);
        okBtn.SetActive(false);
        PhotonNetwork.LeaveRoom();
        Debug.Log("Left Room");
    }  
}