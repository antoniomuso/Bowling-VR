using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using IgnoreHovering = Valve.VR.InteractionSystem.IgnoreHovering;

public class TransferOwnership : MonoBehaviour
{
    public bool ballOwner = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    private void DisableHovering() {
        this.ballOwner = true;
        //this.GetComponent<Rigidbody>().useGravity = false;

        if (this.GetComponent<PhotonView>().IsMine) {
            //this.GetComponent<Rigidbody>().isKinematic = false;
        } else {
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.AddComponent<IgnoreHovering>();
        }
    }

    public void RequestOwnership()
    {
        if (this.GetComponent<PhotonView>().IsMine) {
            this.GetComponent<PhotonView>().RPC("DisableHovering", RpcTarget.All);
        } else {
            this.GetComponent<PhotonView>().RequestOwnership();
        }
    }

    [PunRPC]
    private void EnableHovering() {
        this.ballOwner = false;
        //this.GetComponent<Rigidbody>().useGravity = true;

        if (this.GetComponent<PhotonView>().IsMine) {
            this.GetComponent<Rigidbody>().isKinematic = false;
        } else {
            //this.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(this.GetComponent<IgnoreHovering>());
        }
    }

    public void ReleaseOwnership() {
        //this.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.MasterClient);
        this.GetComponent<PhotonView>().RPC("EnableHovering", RpcTarget.All);
    }
}
