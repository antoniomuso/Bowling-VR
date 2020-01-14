using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TransferOwnership : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
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

/*
    public void OnTransferOwnership()
    {
        this.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
    }
*/

    public void OnOwnershipRequest(PhotonView view, Player reqPlayer)
    {

        Debug.Log("OnOwnershipRequest(): Player " + reqPlayer + " requests ownership of: " + view + ".");
        // if (this.TransferOwnershipOnRequest)
        // {
            //view.TransferOwnership(reqPlayer);
        // }
    }

    public void OnOwnershipTransfered(PhotonView view, Player prevPlayer) {

        Debug.Log("OnOwnershipTransfered(): Player " + prevPlayer + " requests ownership of: " + view + ".");
    }

}
