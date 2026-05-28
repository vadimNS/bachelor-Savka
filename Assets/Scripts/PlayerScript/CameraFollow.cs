using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;     
    [SerializeField] private float smoothSpeed = 0.125f; 
    [SerializeField] private Vector3 offset;       
    [SerializeField] private float yDeadZone = 0.5f; 

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 currentPosition = transform.position;

        
        float yDifference = Mathf.Abs(desiredPosition.y - currentPosition.y);
        if (yDifference < yDeadZone)
        {
            desiredPosition.y = currentPosition.y;
        }

        
        Vector3 smoothedPosition = Vector3.Lerp(currentPosition, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
