using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is never used, but I spent so much time that I don't feel like deleting it, so fuck it, useless code gang
public abstract class UnificationSpawnerSpawnable<T, U> : MonoBehaviour where T : Spawnables<T, U>
{

}
[SerializeField]
public class Spawner<T> : MonoBehaviour where T : Spawnables<T, Spawner<T>>
{/*
    public T thing;
    ObjectPool<T> _pool;

    public virtual void Start()
    {
        //thing = Spawnables<T, Spawner<T>>.thing;
        _pool = new ObjectPool<T>(Factory, Spawnables<T, Spawner<T>>.Enable, Spawnables<T, Spawner<T>>.Disable, 3);
    }
    public T Factory()
    {
        return Instantiate(thing);
    }
    void Update()
    {

    }

    public T GetOne()
    {
        thing = _pool.GetObject();
        if (!thing.spawner)
        {
            thing.SetSpawner(this, thing);
            thing.Create(_pool);
        }
        return thing;
    }

    public void EndOne(T obj)
    {
        _pool.ReturnObject(obj);
    }*/
}
