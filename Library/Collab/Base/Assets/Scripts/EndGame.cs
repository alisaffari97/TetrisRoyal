using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EndGame : MonoBehaviour
{
    public static bool GameIsEnded = false;

    [SerializeField]
    public GameObject pauseMenu;
    [SerializeField]
    public GameObject btnEnd;

    // Update is called once per frame
    void Update()
    {
        if (TetrisBlock.gameOver)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void BtnEnd()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
