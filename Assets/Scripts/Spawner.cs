using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Spawner : MonoBehaviour
{
    public GameObject[] Tetrominoes;

    public GameObject gameLabel;
    public GameObject winnerText;
    public GameObject winnerbutton;

    public static bool gameisOver = false;

    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        gameisOver = false;
        PV = GetComponent<PhotonView>();
        CreateBar();
        NewTetromino();
        winnerText.SetActive(false);
    }

    private void Update()
    {
        Debug.Log(TetrisBlock.gameOver);
        if (TetrisBlock.gameOver)
        {
            gameLabel.SetActive(true);
            gameisOver = true;
            PV.RPC("RPC_Function", RpcTarget.Others);
        }
        else gameLabel.SetActive(false);
    }

    [PunRPC]
    void RPC_Function()
    {
        PhotonNetwork.Destroy(GameObject.Find("TrackEnemy"));
        winnerText.SetActive(true);
        winnerbutton.SetActive(true);
        TetrisBlock.ResetVaribales();
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
        Time.timeScale = 1;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonView.Destroy(gameObject);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

}
