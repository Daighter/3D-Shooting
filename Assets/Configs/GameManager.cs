using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const string DefaultName = "Manager";

    private static GameManager instance;
    private static PoolManager poolManager;

    public static GameManager Instance { get { return instance; } }
    public static PoolManager Data { get { return poolManager; } }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("GameInstance: valid instance already registered.");
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        InitManagers();
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private void InitManagers()
    {
        // DataManager init
        GameObject poolObj = new GameObject() { name = "PoolManager" };
        poolObj.transform.SetParent(transform);
        poolManager = poolObj.AddComponent<PoolManager>();
    }
}
