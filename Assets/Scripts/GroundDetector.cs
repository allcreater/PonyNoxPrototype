using UnityEngine;
using System.Collections.Generic;

public class GroundDetector : MonoBehaviour
{
    public Transform[] m_raycastPoints;
    public float m_maxDistance;

    public bool IsGrounded { get; private set; }
    public Vector3 GroundNormal { get; private set; }

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        IsGrounded = false;
        GroundNormal = Vector3.up;

        foreach (Transform raycastTransform in m_raycastPoints)
        {
            RaycastHit hitInfo;

            var ray = new Ray(raycastTransform.position + Vector3.up*0.1f, Vector3.down);
#if UNITY_EDITOR
            Debug.DrawLine(ray.origin, ray.origin + (ray.direction * m_maxDistance));
#endif


            if (Physics.Raycast(ray, out hitInfo, m_maxDistance, 1))
            {
                IsGrounded = true;

                GroundNormal = hitInfo.normal; //TODO
            }
        }
	}
}
