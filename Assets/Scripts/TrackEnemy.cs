using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TrackEnemy : MonoBehaviourPunCallbacks, IPunObservable
{
    private int lineTrack;
    private int lineTrackRcv;

    public GameObject[] Lines;

    private void Start()
    {

    }

    private void Update()
    {
        Debug.Log(lineTrack);
        if (PhotonNetwork.InRoom)
        {
            lineTrack = TetrisBlock.maxLine;
            trackbar(lineTrackRcv);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {   
            stream.SendNext(lineTrack);
        }
        else
        {
            lineTrackRcv = (int)stream.ReceiveNext();
        }
    }

    void trackbar(int lines)
    {
        for (int i = lines; i >= 0; i--)
        {
            Lines[i].SetActive(true);
            for (int j = lines+1; j < Lines.Length; j++)
            {
                Lines[j].SetActive(false);
            }
        }
    }
}
