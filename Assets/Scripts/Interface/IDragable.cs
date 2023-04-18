using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    public GameObject ObjectsToBeDraged(ref Vector3 positions);
    void Move(Vector3 position);
}
