using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Spawner : MonoBehaviour
{
    public GameObject[] Tetrominoes;

    public Transform bar;

    public GameObject gameLabel;
    public GameObject winnerText;

    private PhotonView PV;

    private float health = 1f;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        NewTetromino();
        winnerText.SetActive(false);
    }

    private void Update()
    {
        if (TetrisBlock.lineClear)
        {
            if (health > 0)
            {
                health -= 0.1f;
                bar.localScale = new Vector3(health, 1f);
                
            }
        }

        if (TetrisBlock.gameOver)
        {
            gameLabel.SetActive(true);
            //PV.RPC("RPC_Function", RpcTarget.Others);            
            
        }
        else gameLabel.SetActive(false);


    }

    [PunRPC]
    void RPC_Function()
    {
        winnerText.SetActive(true);
    }

    public void NewTetromino()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
    }
}
