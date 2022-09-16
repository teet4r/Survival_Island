using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public GameObject spark;
    public AudioSource audioSource;
    public AudioClip hitSound;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(hitSound, 1f);
            GameObject copySpark = Instantiate(spark, collision.transform.position, Quaternion.identity);
            Destroy(copySpark, 2f);
        }
    }
}
