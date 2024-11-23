using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    AudioSource audioSource;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;


    bool isControllable = true;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) 
    {
        if (!isControllable) { return; }

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("you hit a friendly object");
                break;
            case "Fuel":
                Debug.Log("you hit fuel object");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Completed":
                StartCompleteSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
        
    }

    void StartCompleteSequence()
    {
        audioSource.Stop();
        PlayFinishSound();
        DisableMovement();
        Invoke("CompleteGame", 2f);
    }

    void PlayFinishSound()
    {
        audioSource.PlayOneShot(successSound);
    }

    void PlayCrashSound()
    {
        audioSource.PlayOneShot(crashSound);
    }

    void StartSuccessSequence()
    {
        isControllable = false;
        DisableMovement();
        audioSource.Stop();
        PlayFinishSound();
        successParticles.Play();
        Invoke("NextLevel", 2f);
    }

    void DisableMovement()
    {
        GetComponent<Movement>().enabled = false;
    }

    void StartCrashSequence()
    {
        DisableMovement();
        audioSource.Stop();
        PlayCrashSound();
        explosionParticles.Play();
        Invoke("ReloadLevel", delay);
    }

    void NextLevel()
    {
        DisableMovement();
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        DisableMovement();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }


}
