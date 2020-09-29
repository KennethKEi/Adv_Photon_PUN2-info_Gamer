using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{

    //private PhotonView PV;
    // the null error comes from here, because the PV is called here at Start, fix could be move it to a awake method or make PhotonView PV to Public 
    public PhotonView PV;
    public GameObject myAvatar;
    public int myTeam;


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>(); 

        if(PV.IsMine)
        {
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }
        

      
    }

    // Update is called once per frame
    void Update()
    {
        if(myAvatar == null && myTeam !=0)
        { 
        if(myTeam ==1)
        {

        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamOne.Length);

        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
                GameSetup.GS.spawnPointsTeamOne[spawnPicker].position, GameSetup.GS.spawnPointsTeamOne[spawnPicker].rotation, 0);
        }

        }

        if (myTeam == 2)
        {

            int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamTwo.Length);

            if (PV.IsMine)
            {
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
                    GameSetup.GS.spawnPointsTeamTwo[spawnPicker].position, GameSetup.GS.spawnPointsTeamTwo[spawnPicker].rotation, 0);
            }

        }
        }
    }

    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = GameSetup.GS.nextPlayersTeam;
        GameSetup.GS.UpdateTeam();
        PV.RPC("RPC_sentTeam", RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void RPC_sentTeam(int whichTeam)
    {
        myTeam = whichTeam;
    }
}
