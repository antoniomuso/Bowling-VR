using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkController : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
     /******************************************************
     * Refer to the Photon documentation and scripting API for official definitions and descriptions
     * 
     * Documentation: https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro
     * Scripting API: https://doc-api.photonengine.com/en/pun/v2/index.html
     * 
     * If your Unity editor and standalone builds do not connect with each other but the multiple standalones
     * do then try manually setting the FixedRegion in the PhotonServerSettings during the development of your project.
     * https://doc.photonengine.com/en-us/realtime/current/connection-and-authentication/regions
     *
     * ******************************************************/

    // Start is called before the first frame update

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connects to Photon master servers
        //Other ways to make a connection can be found here: https://doc-api.photonengine.com/en/pun/v2/class_photon_1_1_pun_1_1_photon_network.html
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        // PhotonNetwork.JoinRoom("test");

        RoomOptions options = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("test", options, TypedLobby.Default);
    }
    
    /*
    private void switchMaster() {
        if (PhotonNetwork.LocalPlayer.IsMasterClient) {
            Debug.Log("Is Master");
            foreach (GameObject pin in pins.pins) {
                pin.GetComponent<Rigidbody>().isKinematic = false;
                pin.GetComponent<Animation>().enabled = true;
            }
            ball.GetComponent<Rigidbody>().isKinematic = false;
            //ball.GetComponent<Rigidbody>().useGravity = false;

        } else {
            Debug.Log("Not Master");
            foreach (GameObject pin in pins.pins) {
                pin.GetComponent<Rigidbody>().isKinematic = true;
                pin.GetComponent<Rigidbody>().useGravity = false;
                pin.GetComponent<Animation>().enabled = false;
            }
            ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    */

/*
    public override void OnMasterClientSwitched(Player newMasterClient) {
        switchMaster();
    }

    public override void OnJoinedRoom() {
        switchMaster();
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log(message);
    }
    */

    public void OnOwnershipRequest(PhotonView view, Player reqPlayer) {
        Debug.Log("OnOwnershipRequest(): Player " + reqPlayer + " requests ownership of: " + view + ".");
        if (!view.gameObject.GetComponent<TransferOwnership>().ballOwner) {
            view.TransferOwnership(reqPlayer);
            view.gameObject.GetComponent<TransferOwnership>().ballOwner = true;
            view.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void OnOwnershipTransfered(PhotonView view, Player prevPlayer) { 
        Debug.Log("Transfer PrevPlayer: " + prevPlayer);
        //ball.GetComponent<Rigidbody>().useGravity = view.gameObject.GetComponent<TransferOwnership>().ballOwner;
        if (view.IsMine) {
            //Debug.Log("OnOwnershipTransfered Local Player: " + PhotonNetwork.LocalPlayer);
            view.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        } else {
            view.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    
}