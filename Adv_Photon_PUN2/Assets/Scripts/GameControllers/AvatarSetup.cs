using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AvatarSetup : MonoBehaviour
{

    private PhotonView PV;
    public int characterValue;
    public GameObject myCharacter;

    public int playerHealth;
    public int playerDamage;

    public Camera myCamera;
    public AudioListener myAL;

    public Animator animator;

    // Use this for initialization
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            AddCharacter(0);
        }
        else
        {
            Destroy(myCamera);
            Destroy(myAL);
        }
    }

    
    void AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        myCharacter = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MaleFreeSimpleMovement1"), transform.position, transform.rotation);
        myCharacter.transform.parent = transform;
        animator = myCharacter.GetComponent<Animator>();
    }
}
