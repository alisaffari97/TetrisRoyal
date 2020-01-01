using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiplayerSceneIndex; //Number for the build index to the multiplay scene.

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.MaxPlayers == 2)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);
                StartGame();
            }
        } else if (PhotonNetwork.CurrentRoom.MaxPlayers == 4)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
            {
                Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);
                StartGame();
            }
        }

    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public override void OnJoinedRoom() //Callback function for when we successfully create or join a room.
    {
        Debug.Log("Joined Room"); 
    }
    private void StartGame() //Function for loading into the multiplayer scene.
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting Game");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex); //because of AutoSyncScene all players who join the room will also be loaded into the multiplayer scene.
        }
    }
}