using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Spawner : MonoBehaviourPunCallbacks
{
    public GameObject[] Tetrominoes;

    public GameObject gameLabel;
    public GameObject winnerText;
    public GameObject winnerbutton;

    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        CreateBar();
        NewTetromino();
        winnerText.SetActive(false);
    }

    private void Update()
    {
        
        if (TetrisBlock.gameOver)
        {
            gameLabel.SetActive(true);
            PV.RPC("RPC_Function", RpcTarget.Others);
        }
        else gameLabel.SetActive(false);
    }

    [PunRPC]
    void RPC_Function()
    {
        Time.timeScale = 0f;
        winnerText.SetActive(true);
        winnerbutton.SetActive(true);
    }

    public void CreateBar()
    {
        PhotonNetwork.Instantiate("TrackEnemy", new Vector3(25, 9, 0), Quaternion.identity);
    }

    public void NewTetromino()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
    }

    public void EndGame()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
