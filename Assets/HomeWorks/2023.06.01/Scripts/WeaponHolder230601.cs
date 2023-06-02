using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder230601 : MonoBehaviour
{
    [SerializeField] Gun230601 gun;

    List<Gun230601> gunList = new List<Gun230601>();

    public void Fire()
    {
        gun.Fire();
    }

    public void Swap(int index)
    {
        gun = gunList[index + 1];
    }
}
