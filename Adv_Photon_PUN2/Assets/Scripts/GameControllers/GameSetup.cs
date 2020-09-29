using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;
    public Transform[] spawnPoints;
    

    private void OnEnable()
    {
        if(GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }

    public void DisconnectPlayer()
    {
        Destroy(Photon_Room.room.gameObject);
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        //PhotonNetwork.Disconnect();
        //while (PhotonNetwork.IsConnected)

        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
        {
            yield return null;
        }
        SceneManager.LoadScene(Multiplayer_Setting.multiplayerSetting.menuScene);
            
    }
}
