using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueController 
{
    void ShowRandomRequest();
    void ShowSpecificRequest(string text, bool end = false);
}
