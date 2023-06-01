using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IHittable
{
    private Rigidbody rb;
    private int hp = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(RaycastHit hit, int damage)
    {
        rb?.AddForceAtPosition(-10 * hit.normal, hit.point, ForceMode.Impulse);

        //Debug.Log("Hitted");
        hp -= damage;/*
        if (hp <= 0)
            Destroy(gameObject);*/
    }
}
