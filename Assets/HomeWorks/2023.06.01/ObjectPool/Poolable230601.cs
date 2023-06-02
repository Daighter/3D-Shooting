using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable230601 : MonoBehaviour
{
    [SerializeField] float releaseTime = 3f;

    private ObjectPool230601 pool;
    public ObjectPool230601 Pool { get { return pool; } set { pool = value; } }

    private void OnEnable()
    {
        StartCoroutine(ReleaseTimer());
    }

    IEnumerator ReleaseTimer()
    {
        yield return new WaitForSeconds(releaseTime);
        pool.Release(this);
    }
}
