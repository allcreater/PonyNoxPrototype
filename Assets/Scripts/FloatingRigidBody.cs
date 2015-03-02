using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FloatingRigidBody : FloatingObjectBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Float(float liquidDensity)
    {
        if (!isActiveAndEnabled)
            return;

        var rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0.0f, 5.0f * liquidDensity, 0.0f)); //TODO add density etc
    }
}
