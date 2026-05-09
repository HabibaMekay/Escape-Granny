using UnityEngine;
using UnityEngine.InputSystem; // You need this!

namespace CameraDoorScript
{
    public class CameraOpenDoor : MonoBehaviour {
        public float DistanceOpen = 3;
        public GameObject text;
        
        // This lets us link to the Simulator's "Select" or "Activate" action
        public InputActionReference interactAction; 

        void Update () {
            RaycastHit hit;
            // Shoots a ray from the camera forward
            if (Physics.Raycast (transform.position, transform.forward, out hit, DistanceOpen)) {
                Debug.Log("I am looking at: " + hit.transform.name);
                // Get the door script (checking parent in case you hit the mesh)
                var door = hit.transform.GetComponentInParent<DoorScript.Door>();
                
                if (door != null) {
                    if (text != null) text.SetActive (true);

                    // TRIGGER LOGIC:
                    // Check 1: Did we press the VR Controller button?
                    // Check 2: Did we press the 'E' key (as a backup)?
                    bool vrButtonPressed = interactAction != null && interactAction.action.triggered;
                    
                    if (vrButtonPressed || Input.GetKeyDown(KeyCode.E)) {
                        door.OpenDoor();
                    }
                } else {
                    if (text != null) text.SetActive (false);
                }
            } else {
                if (text != null) text.SetActive (false);
            }
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace CameraDoorScript
// {
// public class CameraOpenDoor : MonoBehaviour {
// 	public float DistanceOpen=3;
// 	public GameObject text;
// 	// Use this for initialization
// 	void Start () {
		
// 	}
	
// 	// Update is called once per frame
// 	void Update () {
// 		RaycastHit hit;
// 		if (Physics.Raycast (transform.position, transform.forward, out hit, DistanceOpen)) {
// 				if (hit.transform.GetComponent<DoorScript.Door> ()) {
// 				text.SetActive (true);
// 				if (Input.GetKeyDown(KeyCode.E))
// 					hit.transform.GetComponent<DoorScript.Door> ().OpenDoor();
// 			}else{
// 				text.SetActive (false);
// 			}
// 		}else{
// 			text.SetActive (false);
// 		}
// 	}
// }
// }
