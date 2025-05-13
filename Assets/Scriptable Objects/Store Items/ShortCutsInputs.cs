using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCutsInputs : MonoBehaviour
{
    public static ShortCutsInputs Instance;

    //Bools
    private bool bookCut = false;



    //Reference For Shortcuts
    public TouchInteraction book;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (bookCut && Input.GetKeyDown(KeyCode.Space))
        {
            if (book != null)
            {
                book.ToggleObjectActivation();
            }
        }
    }

    public void SetShortcutState(string shortcutName, bool state)
    {
        switch (shortcutName)
        {
            case "Book":
                bookCut = state;
                break;
            default:
                Debug.LogWarning("Shortcut no reconocido: " + shortcutName);
                break;
        }
    }
}
