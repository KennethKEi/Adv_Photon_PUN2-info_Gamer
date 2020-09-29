using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarCombat : MonoBehaviour
{

    private PhotonView PV;
    private AvatarSetup avatarSetup;
    public  Transform rayOrigin;


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        avatarSetup = GetComponent<AvatarSetup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PV.IsMine)
        {
            return;
        }

        if(Input.GetMouseButton(0)) // 0 is the value of left mouse button
        {
            PV.RPC("RPC_Shooting", RpcTarget.All);
     
        }
    }
    [PunRPC]
    void RPC_Shooting()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) 
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.InverseTransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did hit");
            if (hit.transform.tag == "Avatar")
            {
                hit.transform.gameObject.GetComponent<AvatarSetup>().playerHealth -= avatarSetup.playerDamage;
            }

        }
        else
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.InverseTransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log("Did not Hit");
        }
    }
}
