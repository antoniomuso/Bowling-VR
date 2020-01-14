using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
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

        
    public void RequestOwnership()
    {
         this.GetComponent<PhotonView>().RequestOwnership();
    }

    public void ReleaseOwnership() {
        //this.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.MasterClient);
        if (this.GetComponent<PhotonView>().IsMine) {
            this.ballOwner = false;
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
