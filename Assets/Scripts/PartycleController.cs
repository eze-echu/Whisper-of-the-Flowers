using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartycleController : MonoBehaviour, IPartycleController
{

    public List<ParticleSystem> particles;
    public Camera _camera;

    private void Start()
    {
        StopAllParticles();
    }

    public void StopParticle(int particle)
    {
        //_camera.enabled = false;
        particles[particle].Stop();
    }

    public void PlayParticle(int particle)
    {
        _camera.enabled = true;
        particles[particle].Play();
    }

    public void StopAllParticles()
    {
        _camera.enabled = false;

        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
        }
    }
}
