using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Transform pivotPoint;  // Custom pivot point (assignable in Inspector)
    public float amplitude = 30f; // Maximum rotation angle
    public float frequency = 1f;  // Speed of oscillation
    public float fixedTime = 0f ;
    private float startRotation;  // Initial rotation angle
    private bool isEnabled = true ;
    void Start()
    {
        // Record the initial rotation angle
        if (pivotPoint == null)
        {
            Debug.LogWarning("Pivot Point not assigned. Using object's position as the pivot.");
            pivotPoint = transform; // Default to the object's position if no pivot is assigned
        }

        startRotation = transform.eulerAngles.z;
    }

    void Update()
    {
        if( isEnabled){
            // Calculate the new angle using a sine wave
            fixedTime += Time.deltaTime ;
             float angle = Mathf.Sin(fixedTime * frequency) * amplitude;

            // Rotate around the pivot point
            RotateAroundPivot(angle);
        }
    }

    void RotateAroundPivot(float angle)
    {
        // Save the current position of the pivot
        Vector3 pivotPos = pivotPoint.position;

        // Move the object relative to the pivot, apply the rotation, then move it back
        transform.RotateAround(pivotPos, Vector3.forward, angle - startRotation);

        // Update the starting rotation for the next frame
        startRotation = angle;
    }

    public void Enable(){
        isEnabled = true ;
    }

    public void Disable(){
        isEnabled = false ;
    }
}
