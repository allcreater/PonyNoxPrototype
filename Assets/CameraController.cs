using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform target;
    public float targetHeight = 2.0f;
    public float distance = 2.8f;
    public float maxDistance = 10f;
    public float minDistance = 0.5f;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -40;
    public float yMaxLimit = 80;
    public float zoomRate = 20;
    public float rotationDampening = 3.0f;

    private float x = 0.0f;
    private float y = 0.0f;
    public bool isTalking = false;

    void Start ()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

       // Make the rigid body not change rotation
        if (rigidbody)
            rigidbody.freezeRotation = true;
    }

    void LateUpdate ()
    {
        if(!target)
            return;
       
       // If either mouse buttons are down, let them govern camera position
       if (Input.GetMouseButton(1) || (Input.GetMouseButton(1)))
       {
           x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
           y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
       }
       else if(Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f) // otherwise, ease behind the target if any of the directional keys are pressed
       {
          //var targetRotationAngle = target.eulerAngles.y;
          //var currentRotationAngle = transform.eulerAngles.y;
          //x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);
        }
         

       distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
       distance = Mathf.Clamp(distance, minDistance, maxDistance);
       
       y = ClampAngle(y, yMinLimit, yMaxLimit);
       
      // ROTATE CAMERA:
       Quaternion rotation = Quaternion.Euler(y, x, 0);
       transform.rotation = rotation;
       
       // POSITION CAMERA:
       var position = target.position - (rotation * Vector3.forward * distance + new Vector3(0,-targetHeight,0));
       transform.position = position;
       
       // IS VIEW BLOCKED?
       RaycastHit hit;
       Vector3 trueTargetPosition = target.transform.position - new Vector3(0, -targetHeight, 0);
       // Cast the line to check:
       if (Physics.Linecast (trueTargetPosition, transform.position, out hit))
       {
          // If so, shorten distance so camera is in front of object:
          var tempDistance = Vector3.Distance (trueTargetPosition, hit.point) - 0.28f;
          // Finally, rePOSITION the CAMERA:
          position = target.position - (rotation * Vector3.forward * tempDistance + new Vector3(0, -targetHeight, 0));
          transform.position = position;
       }
    }

    static float ClampAngle (float angle, float min, float max)
    {
       if (angle < -360)
          angle += 360;
       if (angle > 360)
          angle -= 360;

       return Mathf.Clamp (angle, min, max);
    }
}
