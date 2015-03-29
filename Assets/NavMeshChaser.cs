using UnityEngine;
using System.Collections.Generic;

public class NavMeshChaser : MonoBehaviour
{
    public Transform m_Target;

    private NavMeshAgent m_nma;
	// Use this for initialization
	void Start ()
	{
        m_nma = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        m_nma.SetDestination(m_Target.position);
	}
}
