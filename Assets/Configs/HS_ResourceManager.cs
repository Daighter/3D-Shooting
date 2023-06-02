using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_ResourceManager : MonoBehaviour
{
    public T Instantiate<T>(T original, Vector3 position, Quaternion rotation, bool pooling = false) where T : Object
    {
        if (pooling)
            return GameManager.Pool.Get(original, position, rotation);
        else
            return Object.Instantiate(original, position, rotation);
    }

    public void Destroy(GameObject go)
    {
        if (GameManager.Pool.Release(go))
            return;
        
        GameObject.Destroy(go);
    }
}
