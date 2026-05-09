using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    [Tooltip("Set your desired FPS limit here.")]
    public int targetFPS = 40;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;

        Application.targetFrameRate = targetFPS;
    }
}