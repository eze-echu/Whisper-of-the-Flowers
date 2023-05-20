using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class AppEvents
{
    public static UnityEvent CloseBookEvent = new UnityEvent();

    //ACA
    public static UnityEvent OpenBookEvent = new UnityEvent();


    public static void InvokeCloseBookEvent()
    {
        CloseBookEvent.Invoke();
    }

    //ACA
    public static void InvokeOpenBookEvent()
    {
        OpenBookEvent.Invoke();
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
