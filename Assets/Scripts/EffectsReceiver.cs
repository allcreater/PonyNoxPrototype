using UnityEngine;
using System.Collections;

public class EffectsReceiver : MonoBehaviour
{

    public GameObject AddEffect (GameObject effectTemplate)
    {
        var effectInstance = EffectBehaviour.InstantiateEffect(effectTemplate, gameObject);
        return effectInstance;
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
