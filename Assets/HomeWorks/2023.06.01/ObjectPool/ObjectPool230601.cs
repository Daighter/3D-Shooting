using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool230601 : MonoBehaviour
{
    [SerializeField] Poolable230601 poolablePrefab;

    [SerializeField] int poolSize = 5;
    [SerializeField] int maxSize = 10;

    private Stack<Poolable230601> objectPool = new Stack<Poolable230601>();

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
       for (int i = 0; i < poolSize; i++)
        {
            Poolable230601 poolable = Instantiate(poolablePrefab);
            poolable.gameObject.SetActive(false);
            poolable.transform.SetParent(transform);
            poolable.Pool = this;
            objectPool.Push(poolable);
        }
    }

    public Poolable230601 Get()
    {
        if (objectPool.Count > 0)
        {
            Poolable230601 poolable = objectPool.Pop();
            poolable.gameObject.SetActive(true);
            poolable.transform.parent = null;
            return poolable;
        }
        else
        {
            Poolable230601 poolable = Instantiate(poolablePrefab);
            poolable.Pool = this;
            return poolable;
        }
    }

    public void Release(Poolable230601 poolable)
    {
        if (objectPool.Count < maxSize)
        {
            poolable.gameObject.SetActive(false);
            poolable.transform.SetParent(transform);
            objectPool.Push(poolable);
        }
        else
            Destroy(poolable.gameObject);
    }
}
