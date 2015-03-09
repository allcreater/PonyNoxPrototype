using UnityEngine;
using System.Collections.Generic;

public class Water : MonoBehaviour
{
	private HashSet<FloatingRigidBody> registeredObjects = new HashSet<FloatingRigidBody>();

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		foreach (FloatingRigidBody obj in registeredObjects)
            obj.Float(1.0f);
	}

    void OnTriggerEnter(Collider other)
    {
		var hm = other.GetComponent<FloatingRigidBody>();
        if (hm != null)
        {
            //Debug.Log("Object enter water");
            registeredObjects.Add(hm); 
        }
    }

    void OnTriggerExit(Collider other)
    {
		var hm = other.GetComponent<FloatingRigidBody>();
        if (hm != null)
        {
            //Debug.Log("Object enter water");
            registeredObjects.Remove(hm);
        }
    }
}
