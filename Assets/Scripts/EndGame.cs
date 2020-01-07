using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (TetrisBlock.gameOver)
        {
            pauseMenu.SetActive(true);
        }       
    }

    public void endGame()
    {
        PhotonNetwork.Destroy(GameObject.Find("TrackEnemy"));
        PhotonNetwork.LeaveRoom();
        StartCoroutine(delayEnd());
    }

    public IEnumerator delayEnd()
    {
        yield return new WaitForSeconds(2);
        PhotonNetwork.LoadLevel(0);
    }
}
