using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// For use with Photon and SteamVR
public class NetworkedPlayer : MonoBehaviourPunCallbacks//, IPunObservable
{
    public GameObject avatar;
    public GameObject rightHand;
    public GameObject leftHand;

    public Transform playerGlobal;
    public Transform playerLocalHead;
    public Transform playerLocalRightHand;
    public Transform playerLocalLeftHand;

    void Start()
    {
        Debug.Log("Player instantiated");

        if (photonView.IsMine)
        {
            Debug.Log("Player is mine");

            playerGlobal = GameObject.Find("Player").transform;
            //playerLocalHead = playerGlobal.Find("NoSteamVRFallbackObjects/FallbackObjects");

            playerLocalRightHand = playerGlobal.Find("SteamVRObjects/RightHand");
            playerLocalLeftHand = playerGlobal.Find("SteamVRObjects/LeftHand");

            //this.transform.SetParent(playerLocalHead);

            rightHand.transform.SetParent(playerLocalRightHand);
            leftHand.transform.SetParent(playerLocalLeftHand);
            

            //this.transform.localPosition = Vector3.zero;
            //hand.transform.localPosition = Vector3.zero;
        }
    }
/*
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocalHead.localPosition);
            stream.SendNext(playerLocalHead.localRotation);
            stream.SendNext(playerLocalHand.localPosition);
            stream.SendNext(playerLocalHand.localRotation);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
            hand.transform.localPosition = (Vector3)stream.ReceiveNext();
            hand.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }
*/
}