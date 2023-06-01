using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class Fire : MonoBehaviour
{
    [SerializeField] Rig aimRig;
    [SerializeField] float reloadTime;
    [SerializeField] WeaponHolder weaponHolder;

    private Animator anim;
    private bool isFire = false;
    private bool reload = true;
    private bool isReloading = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFire && reload && !isReloading)
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
        weaponHolder.Fire();
        yield return new WaitForSeconds(0.1f);
        reload = true;
    }

    private void OnReload(InputValue value)
    {
        if (isReloading)
            return;

        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        anim.SetTrigger("Reload");
        isReloading = true;
        aimRig.weight = 0f;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        aimRig.weight = 1f;
    }
}
