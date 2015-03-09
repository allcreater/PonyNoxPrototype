using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FloatingRigidBody : MonoBehaviour
{
	public float m_density = 1.0f;

	private Rigidbody m_rigidBody;
    // Use this for initialization
    void Start()
    {
		m_rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Float(float liquidDensity)
    {
        if (!isActiveAndEnabled)
            return;

		float volume = m_rigidBody.mass / m_density;
        m_rigidBody.velocity = m_rigidBody.velocity * 0.99f; //dampening
		m_rigidBody.AddForce(new Vector3(0.0f, volume * liquidDensity, 0.0f)); //TODO add density etc
    }
}
