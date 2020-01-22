using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// For use with Photon and SteamVR
public class NetworkedPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject avatar;
    public GameObject hand;

    public Transform playerGlobal;
    public Transform playerLocalHead;
    public Transform playerLocalHand;

    void Start()
    {
        Debug.Log("Player instantiated");

        if (photonView.IsMine)
        {
            Debug.Log("Player is mine");

            playerGlobal = GameObject.Find("Player").transform;
            playerLocalHead = playerGlobal.Find("NoSteamVRFallbackObjects/FallbackObjects");
            playerLocalHand = playerGlobal.Find("NoSteamVRFallbackObjects/FallbackHand");

            this.transform.SetParent(playerLocalHead);
            hand.transform.SetParent(playerLocalHand);
            

            this.transform.localPosition = Vector3.zero;
            hand.transform.localPosition = Vector3.zero;
        }
    }

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

    
}