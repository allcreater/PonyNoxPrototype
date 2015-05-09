using UnityEngine;
using System.Collections.Generic;

public class NavMeshChaser : MonoBehaviour
{
    public Transform m_Target;

    private NavMeshNavigator m_nmn;
    private Rigidbody m_rb;
	// Use this for initialization
	void Start ()
	{
        m_rb = GetComponent<Rigidbody>();
        m_nmn = GetComponentInChildren<NavMeshNavigator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        m_nmn.Target = m_Target.position;

        m_rb.AddForce(m_nmn.DesiredDirection * 10.0f, ForceMode.Acceleration);
	}
}
