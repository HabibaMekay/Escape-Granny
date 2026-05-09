using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; 

public class EnemyAI : MonoBehaviour
{
    public Transform player; 
    private NavMeshAgent agent;
    public float catchDistance = 1.5f; 

    public GameObject gameOverUI; 
    private bool isCaught = false; 

    public AudioSource heartbeatAudio;
    public float heartbeatStartDistance = 10f; 

    // NEW: We need this to control the animations!
    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        if (heartbeatAudio != null)
        {
            heartbeatAudio.volume = 0f;
        }
    }

    void Update()
    {
        if (player != null && isCaught == false)
        {
            agent.SetDestination(player.position);
            float distance = Vector3.Distance(transform.position, player.position);
            
            // NEW: Send the NavMeshAgent's speed to the Animator so he walks
            if (animator != null && agent != null)
            {
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }

            if (heartbeatAudio != null)
            {
                if (distance <= heartbeatStartDistance)
                {

                    if (!heartbeatAudio.isPlaying)
                    {
                        heartbeatAudio.Play();
                    }

                    // 34n el sootel heartbeat mykonsh cut off suddenly
                    float dynamicVolume = Mathf.InverseLerp(heartbeatStartDistance, catchDistance, distance);
                    heartbeatAudio.volume = dynamicVolume;
                }
                else
                {

                    if (heartbeatAudio.isPlaying)
                    {
                        heartbeatAudio.Pause();
                    }
                }
            }

            if (distance <= catchDistance)
            {
                GameOverSequence();
            }
        }
    }

    void GameOverSequence()
    {
        isCaught = true; 
        
        if (agent.isActiveAndEnabled)
        {
            agent.isStopped = true; 
        }

        // NEW: Force the speed to 0 so he stops walking and plays his Idle animation
        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
        }

        if (heartbeatAudio != null)
        {
            heartbeatAudio.Stop();
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

}