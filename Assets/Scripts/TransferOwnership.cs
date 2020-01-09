using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TransferOwnership : MonoBehaviourPunCallbacks
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

    public void OnOwnershipRequest(object[] viewAndPlayer)
    {
        PhotonView view = viewAndPlayer[0] as PhotonView;
        Player requestingPlayer = viewAndPlayer[1] as Player;

        Debug.Log("OnOwnershipRequest(): Player " + requestingPlayer + " requests ownership of: " + view + ".");
        // if (this.TransferOwnershipOnRequest)
        // {
            view.TransferOwnership(requestingPlayer);
        // }
    }

}
