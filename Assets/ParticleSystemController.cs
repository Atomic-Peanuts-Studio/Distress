using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public ParticleSystem s;
   public void CleanParticles()
    {
        
        s.Clear();
    }
}
