using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.SceneManagement; 
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu & Game Over")]
    public GameObject startMenuUI; 
    public GameObject gameOverUI; 
    public EnemyAI monsterScript;  
    public GameObject startHints;

    [Header("Pause Menu Elements")]
    public GameObject pauseCanvas;
    
    [Header("Pause Input")]
    public InputActionReference pauseButton; 

    private bool isPaused = false;

    [Header("Player Movement Setup")]
    public ContinuousMoveProvider moveProvider; // --- NEW: Slot for the VR move script ---

    [Header("Difficulty Speeds")]
    public float easySpeed = 3f;
    public float mediumSpeed = 2f;
    public float hardSpeed = 1f;


    void Start()
    {
            // --- NEW: Set a default speed in case they hit "Start" without picking a difficulty ---
        if (moveProvider != null)
        {
            moveProvider.moveSpeed = mediumSpeed; 
        }

        // Handle Start Menu
        if (startMenuUI != null)
        {
            startMenuUI.SetActive(true);
        }

        // Handle Game Over Menu
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // Handle Pause Menu (Hide at start)
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        }

        // Handle Monster
        if (monsterScript != null)
        {
            monsterScript.enabled = false;
        }
    }

    // --- PAUSE MENU INPUT SETUP ---
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
        // Listen for the 'X' key on the keyboard
        if (Keyboard.current != null && Keyboard.current.xKey.wasPressedThisFrame)
        {
            TogglePauseLogic();
        }
    }

    // --- THE PAUSE LOGIC ---
    private void TogglePauseLogic()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Show the menu and freeze time
            if (pauseCanvas != null) pauseCanvas.SetActive(true);
            Time.timeScale = 0f; 
        }
        else
        {
            // Hide the menu and unfreeze time
            if (pauseCanvas != null) pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    // --- START GAME BUTTON LOGIC ---
    public void StartGameButton()
    {
        if (startMenuUI != null)
        {
            startMenuUI.SetActive(false);
        }

        if (monsterScript != null)
        {
            monsterScript.enabled = true;
        }

// --- NEW: Trigger the hint sequence ---
        if (startHints != null)
        {
            StartCoroutine(ShowHintsRoutine());
        }
    }

IEnumerator ShowHintsRoutine()
    {
        // Show the hints
        startHints.SetActive(true);

        // Wait for exactly 7 seconds
        yield return new WaitForSeconds(7f);

        // Hide the hints
        startHints.SetActive(false);
    }


    // --- RESUME GAME BUTTON LOGIC ---
    public void ResumeGame()
    {
        // Force the game to unpause
        isPaused = false;

        // Hide the menu and unfreeze time
        if (pauseCanvas != null) 
        {
            pauseCanvas.SetActive(false);
        }
        Time.timeScale = 1f;
    }


    public void RestartLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    public void SecondRestartLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

public void SetEasyDifficulty()
    {
        if (moveProvider != null) moveProvider.moveSpeed = easySpeed;
        Debug.Log("Difficulty: Easy. Speed is now " + easySpeed);
    }

    public void SetMediumDifficulty()
    {
        if (moveProvider != null) moveProvider.moveSpeed = mediumSpeed;
        Debug.Log("Difficulty: Medium. Speed is now " + mediumSpeed);
    }

    public void SetHardDifficulty()
    {
        if (moveProvider != null) moveProvider.moveSpeed = hardSpeed;
        Debug.Log("Difficulty: Hard. Speed is now " + hardSpeed);
    }
}