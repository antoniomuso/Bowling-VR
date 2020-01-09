using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void OnTransferOwnership()
    {
        this.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
    }

/*  
    public void OnOwnershipRequest(object[] viewAndPlayer)
    {
        PhotonView view = viewAndPlayer[0] as PhotonView;
        PhotonPlayer requestingPlayer = viewAndPlayer[1] as PhotonPlayer;

        Debug.Log("OnOwnershipRequest(): Player " + requestingPlayer + " requests ownership of: " + view + ".");
        if (this.TransferOwnershipOnRequest)
        {
            view.TransferOwnership(requestingPlayer.ID);
        }
    }
*/
}
