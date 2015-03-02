using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class FloatingCharacterController : FloatingObjectBehaviour
{
    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    public override void Float(float liquidDensity)
    {
        if (!isActiveAndEnabled)
            return;

        var characterController = GetComponent<CharacterController>();
        characterController.Move(new Vector3(0.0f, 5.0f * liquidDensity * Time.deltaTime, 0.0f));
    }
}
