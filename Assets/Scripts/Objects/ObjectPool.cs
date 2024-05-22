using System.Collections.Generic;
using UnityEngine;
 
public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T prefab;
    private readonly Queue<T> objects = new Queue<T>();

    public ObjectPool(T prefab, int initialSize)
    {
        this.prefab = prefab;
        for (int i = 0; i < initialSize; i++)
        {
            var newObj = GameObject.Instantiate(prefab);
            newObj.gameObject.SetActive(false);
            objects.Enqueue(newObj);
        }
    }

    public T GetObject()
    {
        if (objects.Count > 0)
        {
            var obj = objects.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = GameObject.Instantiate(prefab);
            return newObj;
        }
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }
}