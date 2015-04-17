using UnityEngine;
using System.Collections.Generic;

public class NavMeshChaser : MonoBehaviour
{
    public Transform m_Target;

    private NavMeshAgent m_nma;
    private Rigidbody m_rb;
	// Use this for initialization
	void Start ()
	{
        m_rb = GetComponent<Rigidbody>();
        m_nma = GetComponentInChildren<NavMeshAgent>();
        m_nma.updateRotation = false;
        m_nma.updatePosition = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
        m_nma.SetDestination(m_Target.position);
        m_rb.AddForce(m_nma.desiredVelocity, ForceMode.Acceleration);
	}
}
