using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTester230601 : MonoBehaviour
{
    private ObjectPool230601 objectPool;

    private void Awake()
    {
        objectPool = GetComponent<ObjectPool230601>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Poolable230601 poolable = objectPool.Get();
            poolable.transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        }
    }
}
