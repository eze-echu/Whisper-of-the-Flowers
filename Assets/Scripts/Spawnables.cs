using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Spawnables<T, U> : MonoBehaviour, ISpawnable<T> where T : ISpawnable<T>
{
    private float _counter;
    public int maxTime;
    public T self;
    protected ObjectPool<T> _referenceBack;



    // Update is called once per frame
    public virtual void Update()
    {
        _counter += Time.deltaTime;
        if (_counter >= maxTime && gameObject.activeSelf)
        {
            //print("Se devuelve objeto");
            _referenceBack.ReturnObject(self);
        }

    }
    /*public void SetSpawner(U spawner, Spawnables<T, U> obj)
    {
        obj.spawner = spawner;
    }*/
    public void Create(ObjectPool<T> op)
    {
        _referenceBack = op;
    }
    public void ResetCounter()
    {
        _counter = 0;
    }
    public static void Enable(Spawnables<T, U> obj)
    {
        obj.gameObject.SetActive(true);
    }
    public static void Disable(Spawnables<T, U> obj)
    {
        obj.gameObject.SetActive(false);
        obj.ResetCounter();
    }

    public void SaveThing(T newThing)
    {
        self = newThing;
    }
}

public interface ISpawnable<T> {
    public void SaveThing(T newThing);
}

