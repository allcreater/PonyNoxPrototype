using UnityEngine;
using System.Collections.Generic;

public class FireballLogic : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	}
	
	void FixedUpdate ()
	{
        if (Physics.CheckSphere(transform.position, 0.1f))
        {

        }
	}
}
