using UnityEngine;

public class HookTrigger : MonoBehaviour
{
    private RopeController ropeController;

    private void Start()
    {
        // Get the RopeController from the parent
        ropeController = GetComponentInParent<RopeController>();
        if (ropeController == null)
        {
            Debug.LogError("RopeController not found in parent object!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{this.name} Trigger 2D with name={other.name} tag={other.tag}");

        
        if (other.CompareTag("Gold"))
        {
            // Attach the gold object to the hook
            AttachGold(other.gameObject);

            // Stop expanding and start shrinking the rope
            ropeController.SetExpand(false);
            ropeController.SetShrink(true);
            ropeController.SetNugget(other.gameObject);
           
        }
        if (other.CompareTag("Background"))
        {
            // Stop expanding and start shrinking the rope
            ropeController.SetExpand(false);
            ropeController.SetShrink(true);
        }
    }

    private void AttachGold(GameObject goldObject)
    {
        // Make the gold object a child of the hook
        goldObject.transform.SetParent(transform);

        // Optionally reset the position of the gold object relative to the hook
        goldObject.transform.localPosition = Vector3.zero;

        // Optionally, reset the rotation of the gold object to match the hook
        goldObject.transform.localRotation = Quaternion.identity;
         if (goldObject.GetComponent<PolygonCollider2D>() != null)
            {
                Destroy(goldObject.GetComponent<PolygonCollider2D>());
            }
        Debug.Log($"Attached {goldObject.name} to {this.name}");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"[OnTriggerExit] Hook exited collision with: {other.name}, Tag: {other.tag}");
        if (other.CompareTag("Background"))
        {
            Debug.Log("Hook exited the background's trigger!");
        }
    }
}
