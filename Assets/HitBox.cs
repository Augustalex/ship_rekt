using UnityEngine;

public class HitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ON COLLISION HITBOX");
        var cannonManager = other.gameObject.GetComponentInParent<CannonManager>();
        if (cannonManager)
        {
            Debug.Log("FOUND CANNON MANAGER");
            Destroy(transform.parent.gameObject);
            cannonManager.AddCannonBall();
        }
    }
}