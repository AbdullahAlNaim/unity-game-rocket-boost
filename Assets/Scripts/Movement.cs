using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip engineSound;
    AudioSource audioSource;
    Rigidbody rb;

    [SerializeField] ParticleSystem centerThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    void Start() 
    {
        rb = GetComponent<Rigidbody>(); 
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable() 
    {
        thrust.Enable();     
        rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (thrust.IsPressed() == true)
        {
            centerThrustParticles.Play();
            
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engineSound);
            }
        }
        else 
        {
            audioSource.Stop();
        }
    }


    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            leftThrustParticles.Play();
            ApplyRotation(rotationStrength);
        }
        else if (rotationInput > 0)
        {
            rightThrustParticles.Play();
            ApplyRotation(-rotationStrength);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
