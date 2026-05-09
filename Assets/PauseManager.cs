using UnityEngine;
using UnityEngine.InputSystem; 

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseCanvas;
    public Transform playerHead; // Drag your Main Camera here!
    public float menuDistance = 1.5f; // How far away the menu spawns
    
    [Header("Controller Input")]
    public InputActionReference pauseButton; 

    private bool isPaused = false;

    void OnEnable()
    {
        if (pauseButton != null) pauseButton.action.started += TogglePauseFromVR;
    }

    void OnDisable()
    {
        if (pauseButton != null) pauseButton.action.started -= TogglePauseFromVR;
    }

    private void TogglePauseFromVR(InputAction.CallbackContext context)
    {
        TogglePauseLogic();
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.xKey.wasPressedThisFrame)
        {
            TogglePauseLogic();
        }
    }

    private void TogglePauseLogic()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // --- NEW: TELEPORT THE MENU IN FRONT OF YOU ---
            if (playerHead != null)
            {
                // 1. Put it 1.5 meters in front of where the camera is looking
                pauseCanvas.transform.position = playerHead.position + (playerHead.forward * menuDistance);
                
                // 2. Make the canvas rotate so it's looking back at you
                pauseCanvas.transform.rotation = Quaternion.LookRotation(pauseCanvas.transform.position - playerHead.position);
            }

            // Show the menu and freeze time
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f; 
        }
        else
        {
            // Hide the menu and unfreeze time
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}