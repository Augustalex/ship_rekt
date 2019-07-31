using UnityEngine;

public class HitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var cannonManager = other.gameObject.GetComponentInParent<CannonManager>();
        if (cannonManager && cannonManager.CanAddCannonBall())
        {
            Destroy(transform.parent.gameObject);
            cannonManager.AddCannonBall();
        }
    }
}