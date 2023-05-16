using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class AppEvents
{
    public static UnityEvent CloseBookEvent = new UnityEvent();

    public static void InvokeCloseBookEvent()
    {
        CloseBookEvent.Invoke();
    }

    /*
    public static event EventHandler CloseBook;

    public static void CloseBookFunction()
    {
        if(CloseBook != null)
            CloseBook(new object(), new EventArgs());
    }

    */
}
