using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class DefaultTransform : MonoBehaviour
{
    public Matrix4x4 m_DefaultTransform;

    public Quaternion m_DefaultRotation;
    public Vector3 m_DefaultTranslation;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        m_DefaultTransform = transform.localToWorldMatrix;

        m_DefaultRotation = transform.localRotation;
        m_DefaultTranslation = transform.localPosition;
	}
}
