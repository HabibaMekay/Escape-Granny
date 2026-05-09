using UnityEngine;
using System.Collections;

public class winScript : MonoBehaviour
{
    [Header("Win Settings")]
    public GameObject winCanvas;  // Drag your 'Yay' menu here
    public CanvasGroup canvasGroup; // Drag your Canvas here again to control its transparency
    public GameObject monster;    // Drag your monster here to stop it
    public AudioSource winSound;  // Drag the object with your AudioSource here!

    [Header("Optional Settings")]
    public bool destroyMonsterOnWin = true;
    public float fadeSpeed = 3f; // How many seconds it takes for the text to fully appear

    // This function runs automatically when something enters the Grass
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<CharacterController>() != null)
        {
            HandleWin();
        }
    }

    void HandleWin()
    {
        Debug.Log("Winner! Grass touched.");

        // 1. Play the Win Sound!
        if (winSound != null)
        {
            winSound.Play();
        }

        // 2. Stop the Monster
        if (monster != null)
        {
            if (destroyMonsterOnWin)
                Destroy(monster);
            else
                monster.SetActive(false);
        }

        // 3. Show the Win Menu (This will also wake up the fireworks parented to it!)
        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
            
            if (canvasGroup != null)
            {
                // Start completely invisible, then smoothly fade in!
                canvasGroup.alpha = 0f; 
                StartCoroutine(FadeInUI());
            }
        }

        // 4. Unlock Mouse so you can click 'Restart' (if needed for PC testing)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // 5. Disable this script so it doesn't trigger twice
        this.enabled = false;
    }

    // This smoothly increases the Canvas visibility over time
    IEnumerator FadeInUI()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            // Calculate the percentage of the fade (0.0 to 1.0)
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeSpeed);
            yield return null; // Wait until the next frame to continue
        }

        canvasGroup.alpha = 1f; // Lock it to fully visible at the end
    }
}