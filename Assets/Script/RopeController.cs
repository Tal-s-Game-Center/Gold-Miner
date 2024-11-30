using UnityEngine;

public class RopeController : MonoBehaviour
{
    private Transform ropeTransform;                 // The rope's transform
    public Transform anchor;                         // The anchor object connected to the bottom of the rope
    public float expansionSpeed = 1f;                // Speed of expansion
    public KeyCode activationKey = KeyCode.Space;    // Key to trigger expansion
    private Rotator rotator;                         // Rotator component controlling rotation
    private Vector3 initialScale;                    // To store the initial scale
    private Vector3 initialLocalPosition;            // To store the initial local position
    private bool isExpanding = false;                // Flag to indicate if the rope is expanding
    private bool isShrinking = false;                // Flag to indicate if the rope is shrinking
    private GameObject nugget = null ;
    void Start()
    {
        // Locate the rope transform
        ropeTransform = transform.Find("Rope");
        if (ropeTransform == null)
        {
            Debug.LogError("Child 'Rope' not found! Ensure the rope is a child of this object.");
            return;
        }

        // Locate the rotator component
        rotator = GetComponent<Rotator>();
        if (rotator == null)
        {
            Debug.LogError("Rotator component not found! Ensure it is attached to the same GameObject.");
        }

        // Record the initial scale and position
        initialScale = ropeTransform.localScale;
        initialLocalPosition = ropeTransform.localPosition;

        if (anchor == null)
        {
            Debug.LogWarning("Anchor is not assigned! The rope will expand or shrink without syncing an anchor.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(activationKey) && !isExpanding && !isShrinking)
        {
            isExpanding = true;
            rotator.Disable(); // Stop rotation while expanding
        }

        if (isExpanding)
        {
            ExpandRope(expansionSpeed * Time.deltaTime);
        }
        else if (isShrinking)
        {
            ShrinkRope(expansionSpeed * Time.deltaTime);
        }
    }

    void ExpandRope(float amount)
    {
        if (ropeTransform == null) return;

        // Adjust scale along Y-axis
        ropeTransform.localScale += new Vector3(0, amount, 0);

        // Move the rope downwards to ensure only the bottom expands
        ropeTransform.localPosition -= new Vector3(0, amount * 0.5f, 0);

        // Move the anchor to the bottom of the rope
        if (anchor != null)
        {
            anchor.localPosition -= new Vector3(0, amount, 0);
        }
    }

    void ShrinkRope(float amount)
    {
        if (ropeTransform == null) return;

        // Shrink the scale only if it's larger than the initial scale
        if (ropeTransform.localScale.y > initialScale.y)
        {
            ropeTransform.localScale -= new Vector3(0, amount, 0);

            // Move the rope upwards to ensure only the bottom shrinks
            ropeTransform.localPosition += new Vector3(0, amount * 0.5f, 0);

            // Prevent overshooting below the initial scale
            if (ropeTransform.localScale.y < initialScale.y)
            {
                ropeTransform.localScale = initialScale;
                ropeTransform.localPosition = initialLocalPosition; // Reset to initial position
                isShrinking = false; // Stop shrinking
                rotator.Enable();    // Re-enable rotation
                if(nugget != null){
                    Destroy(nugget) ;
                    nugget = null ;
                }
            }

            // Update the anchor's position
            if (anchor != null)
            {
                anchor.localPosition += new Vector3(0, amount, 0);
            }
        }
        else
        {
            isShrinking = false; // Stop shrinking if already at initial scale
        }
    }

    public void SetExpand(bool isExpanding)
    {
        this.isExpanding = isExpanding;
    }

    public void SetShrink(bool isShrinking)
    {
        this.isShrinking = isShrinking;
    }
    public void SetNugget(GameObject nugget){
        this.nugget = nugget ;
    }
}
