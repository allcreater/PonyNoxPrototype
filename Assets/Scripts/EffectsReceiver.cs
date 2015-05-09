using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EffectsReceiver : MonoBehaviour
{
    private Dictionary<string, EffectBehaviour> m_registeredEffects = new Dictionary<string,EffectBehaviour>();

    public GameObject AddEffect (GameObject effectTemplate)
    {
        //var effectInstance = EffectBehaviour.InstantiateEffect(effectTemplate, gameObject);
        //return effectInstance;

        var instance = GameObject.Instantiate(effectTemplate, transform.position, transform.rotation) as GameObject;
        instance.transform.SetParent(transform);

        foreach (var effect in instance.GetComponents<EffectBehaviour>())
        {
            EffectBehaviour existingEffect = null;
            if (m_registeredEffects.TryGetValue(effect.m_SpellID, out existingEffect) && existingEffect)
            {//усилим старый эффект, а новый можно и удалить
                Destroy(effect.gameObject);
            }
            else
            {
                effect.m_Target = gameObject;
                m_registeredEffects[effect.m_SpellID] = effect;
            }
        }

        return instance;
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
