using UnityEngine;

public class ParticleSoundPlayer : MonoBehaviour
{
    public AudioSource AS;
    ParticleSystem PS;

    bool HasPlaySound;

    void Awake()
    {
        PS = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (PS.time <= 0.1f)
        {
            if (!HasPlaySound)
            {
                AS.PlayOneShot(AS.clip);
                HasPlaySound = true;
            }
        }
        else
        {
            HasPlaySound = false;
        }
    }
}