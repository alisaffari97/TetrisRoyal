using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameSetupController : MonoBehaviour
{
    [SerializeField]
    public Text overText;


    // This script will be added to any multiplayer scene
    void Start()
    {

    }

    private void Update()
    {        
        overText.text = "Lantecy: " + PhotonNetwork.GetPing().ToString();
    }
}