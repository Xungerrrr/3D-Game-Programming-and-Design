using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TankMovementAI : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
    public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
    public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.
    public GameObject target;

    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
    private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the Tanks

    private void Awake() {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // We grab all the Particle systems child of that Tank to be able to Stop/Play them on Deactivate/Activate
        // It is needed because we move the Tank when spawning it, and if the Particle System is playing while we do that
        // it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
        m_particleSystems = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < m_particleSystems.Length; ++i) {
            m_particleSystems[i].Play();
        }
    }

    private void OnDisable() {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;

        // Stop all particle system so it "reset" it's position to the actual one instead of thinking we moved when spawning
        for (int i = 0; i < m_particleSystems.Length; ++i) {
            m_particleSystems[i].Stop();
        }
    }


    private void Start() {
        // Store the original pitch of the audio source.
        m_OriginalPitch = m_MovementAudio.pitch;
    }


    private void Update() {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (target != null)
            agent.SetDestination(target.transform.position);
        EngineAudio();
    }


    private void EngineAudio() {
        // if the tank is moving and if the idling clip is currently playing...
        if (m_MovementAudio.clip == m_EngineIdling) {
            // ... change the clip to driving and play.
            m_MovementAudio.clip = m_EngineDriving;
            m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
            m_MovementAudio.Play();
        }
    }
}