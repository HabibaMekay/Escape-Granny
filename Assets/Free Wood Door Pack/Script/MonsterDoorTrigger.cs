using UnityEngine;
using DoorScript; // <-- THIS IS THE MAGIC LINE! It tells Unity where to find your Door.

public class MonsterDoorTrigger : MonoBehaviour
{
    [Tooltip("Drag the object that has your Door script attached here")]
    public Door doorScript;

    // When the monster enters the invisible box
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            // If the door is currently closed, open it!
            if (!doorScript.open)
            {
                doorScript.OpenDoor(); // Calling your custom method so it plays the sound!
            }
        }
    }

    // When the monster leaves the invisible box
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            // Always shut the door behind them as they leave
            if (doorScript.open)
            {
                doorScript.OpenDoor(); // Calling it again to toggle it shut and play the close sound
            }
        }
    }
}