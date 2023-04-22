using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    public bool canBeDragged { get; set; }
    public GameObject ObjectsToBeDraged(ref Vector3 positions);
    public bool WasUsed();
}
