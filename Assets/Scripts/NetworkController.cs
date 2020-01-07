using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    private string userId;
    
    private void Awake()
    {
        userId = "" + Random.Range(-1000000, 1000000);
    }

    void Start()
    {
        PhotonNetwork.AuthValues = new AuthenticationValues(userId);
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log(userId);
    }

    public override void OnConnectedToMaster()
    {

        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
    }

  
}
