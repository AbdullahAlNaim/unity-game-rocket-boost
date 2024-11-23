using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // public static Vector3 Lerp(Vector3 a, Vector3 b, float t);
    Vector3 startPosition;
    Vector3 endPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float Speed;
    float movementFactor;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    void Update() 
    {
        movementFactor = Mathf.PingPong(Time.time * Speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }

}
