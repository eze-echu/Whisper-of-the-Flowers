using System.Collections;
using System.Collections.Generic;
using Flowers;
using Systems;
using UnityEngine;

public class HandInZone : MonoBehaviour 
{
    private delegate void handInActions();
    private handInActions handInAfter;
    private handInActions handInBefore;

    private PartycleController partycleController;
    public AudioSource EffectSound;

    public void Start()
    {
        partycleController = FindObjectOfType<PartycleController>();
    }

    
}
