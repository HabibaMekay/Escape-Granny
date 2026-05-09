using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPitchController : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("Drag your Camera Offset object here")]
    public Transform cameraOffset;
    
    [Tooltip("Bind this to your controller (e.g., Right Stick Y)")]
    public InputActionProperty pitchInput; 

    [Header("Settings")]
    public float lookSpeed = 100f;
    public bool invertY = false;

    private float pitch = 0f;

    // --- THESE TWO NEW BLOCKS WAKE UP THE INPUT ---
    void OnEnable()
    {
        pitchInput.action.Enable();
    }

    void OnDisable()
    {
        pitchInput.action.Disable();
    }
    // ----------------------------------------------

    void Update()
    {
        float verticalLook = pitchInput.action.ReadValue<float>();

        if (Mathf.Abs(verticalLook) > 0.1f) 
        {
            float direction = invertY ? 1f : -1f;
            pitch += verticalLook * lookSpeed * direction * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -85f, 85f);
            
            cameraOffset.localEulerAngles = new Vector3(pitch, cameraOffset.localEulerAngles.y, cameraOffset.localEulerAngles.z);
        }
    }
}