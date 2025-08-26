using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PurchaseFlower", menuName = "Store Effects/Purchase Flower")]
public class PurchaseFlower : StoreEffect
{
    [Header("Flor a desbloquear")]
    public Flower flowerToUnlock;  
    public override void Apply()
    {
        if (flowerToUnlock == null)
        {
            Debug.LogWarning("No se asignó ninguna flor para desbloquear");
            return;
        }

        flowerToUnlock.available = true;
        Debug.Log($"La flor {flowerToUnlock.name} ahora está disponible");
    }
}
