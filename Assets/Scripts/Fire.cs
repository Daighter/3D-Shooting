using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class Fire : MonoBehaviour
{
    private Animator anim;
    private TwoBoneIKConstraint twoBoneIk;
    private bool isFire = false;
    private bool reload = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        twoBoneIk = GetComponentInChildren<TwoBoneIKConstraint>();
    }

    private void Update()
    {
        if (isFire && reload)
            StartCoroutine(KeepFire());
    }

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
            isFire = true;
        else
            isFire = false;
        
    }

    IEnumerator KeepFire()
    {
        reload = false;
        anim.SetTrigger("Fire");
        yield return new WaitForSeconds(0.1f);
        reload = true;
    }

    private void OnReload(InputValue value)
    {
        anim.SetTrigger("Reload");
    }
}
