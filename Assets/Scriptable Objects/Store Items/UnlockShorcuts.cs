using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShortCuts", menuName = "Store Effects/ShortCuts")]
public class UnlockShorcuts : StoreEffect
{
    public enum TypeShortCut
    {   
        Book
    }

    public TypeShortCut type;

    public override void Apply()
    {
        switch (type)
        {
            case TypeShortCut.Book:
                Debug.Log("ShortCut Book Unlocked");
                ShortCutsInputs.Instance.SetShortcutState("Book", true);
                break;

            default:
                Debug.LogWarning("ShortCut Undefined");
                break;
        }

    }
}
