using UnityEngine;
using System.Collections;

public class EffectArea : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
    void OnTriggerStay (Collider otherCollider)
    {
        var receiver = otherCollider.GetComponent<EffectsReceiver>();
        if (!receiver)
            return;

        receiver.AddEffect(m_EffectPrefab);
    }
}
