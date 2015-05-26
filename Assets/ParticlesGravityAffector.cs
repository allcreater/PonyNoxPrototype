using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ParticlesGravityAffector : MonoBehaviour
{

	ParticleSystem m_System;
	ParticleSystem.Particle[] m_Particles;
	public float m_gravityForce = 0.01f;
	
	private void LateUpdate()
	{
		InitializeIfNeeded();
		
		// GetParticles is allocation free because we reuse the m_Particles buffer between updates
		int numParticlesAlive = m_System.GetParticles(m_Particles);
		
		// Change only the particles that are alive
		for (int i = 0; i < numParticlesAlive; i++)
		{
			var direction = m_Particles[i].position ;
			m_Particles[i].velocity -= (direction/(direction.sqrMagnitude + 0.1f) * m_gravityForce);
		}
		
		// Apply the particle changes to the particle system
		m_System.SetParticles(m_Particles, numParticlesAlive);
	}
	
	void InitializeIfNeeded()
	{
		if (m_System == null)
			m_System = GetComponent<ParticleSystem>();
		
		if (m_Particles == null || m_Particles.Length < m_System.maxParticles)
			m_Particles = new ParticleSystem.Particle[m_System.maxParticles]; 
	}
}
