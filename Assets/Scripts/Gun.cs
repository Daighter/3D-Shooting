using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxDistance;
    [SerializeField] int damage;

    private ParticleSystem hitEffect;
    private TrailRenderer bulletTrail;
    private int lookDistance = 20;

    private void Awake()
    {
        hitEffect = Resources.Load<ParticleSystem>("Prefabs/HitEffect");
        bulletTrail = Resources.Load<TrailRenderer>("Prefabs/BulletTrail");
    }

    public virtual void Fire()
    {
        muzzleEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward * lookDistance, out hit, maxDistance))
        {
            IHittable hittable = hit.transform.GetComponent<IHittable>();
            //ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));

            ParticleSystem effect = GameManager.Resource.Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform;
            //Destroy(effect.gameObject, 3f);
            StartCoroutine(ReleaseCoroutine(effect));

            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, hit.point));

            hittable?.TakeDamage(hit, damage);
        }
        else
        {
            StartCoroutine(TrailRoutine(muzzleEffect.transform.position, Camera.main.transform.forward * maxDistance));
        }
        //Debug.Log("Fire");
    }

    IEnumerator ReleaseCoroutine(ParticleSystem effect)
    {
        yield return new WaitForSeconds(3f);
        GameManager.Resource.Destroy(effect.gameObject);
    }

    IEnumerator TrailRoutine(Vector3 startPoint, Vector3 endPoint)
    {
        //TrailRenderer trail = Instantiate(bulletTrail, muzzleEffect.transform.position, Quaternion.identity);
        TrailRenderer trail = GameManager.Resource.Instantiate(bulletTrail, startPoint, Quaternion.identity, true);
        trail.Clear();

        float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

        float rate = 0;
        while (rate < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
            rate += Time.deltaTime / totalTime;

            yield return null;
        }
        GameManager.Resource.Destroy(trail.gameObject);

        yield return null;

        if (!trail.IsValid())
        {
            Debug.Log("트레일이 없다");
        }
        else
        {
            Debug.Log("트레일이 있다");
        }
    }
}
