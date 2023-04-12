using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetVariables
{
    int Intent { get; }
    int Formality { get; }
    int MultiplierIntent { get; }
    int MultiplierFormality { get; }
    string Subject { get; }

}
