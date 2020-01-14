using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PinTrigger : MonoBehaviour
{
    public PinController pins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Ball") {
            if (other.GetComponent<PhotonView>().IsMine) {
                foreach (GameObject pin in pins.pins) {
                    pin.GetComponent<PhotonView>().RequestOwnership();
                    pin.GetComponent<Rigidbody>().isKinematic = false;
                    pin.GetComponent<Animation>().enabled = true;
                }
            } else {
                 foreach (GameObject pin in pins.pins) {
                    pin.GetComponent<Rigidbody>().isKinematic = true;
                    pin.GetComponent<Animation>().enabled = false;
                }
            }
        }
    }


}
