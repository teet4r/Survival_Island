using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletCtrl : MonoBehaviour
{
    Rigidbody rigidbody;
    TrailRenderer trailRenderer;

    public float speed;

    [SerializeField]
    float removeTime;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void OnEnable()
    {
        rigidbody.AddForce(transform.forward * speed);

        //Destroy(gameObject, 3f);
        Invoke("RemoveObject", removeTime);
    }

    void RemoveObject()
    {
        if (!gameObject.activeSelf) return;
        ObjectPool.instance.ReturnBullet(this);
    }

    public void Clear()
    {
        rigidbody.velocity = Vector3.zero;
        trailRenderer.Clear();
    }
}