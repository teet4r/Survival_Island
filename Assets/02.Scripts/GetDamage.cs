using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GetDamage : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;
    AudioSource audioSource;

    [SerializeField]
    [Range(1, 1000)]
    int maxHp;
    int hp;
    [SerializeField]
    Collider[] weaponCollider;

    public GameObject bloodEffect;
    public Image hpBar;
    public Canvas hpCanvas;
    public AudioClip audioClip;

    public bool isDie { get; private set; } = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        hp = maxHp;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            HitAniEffect(collision);
            hp -= 3;
            hpBar.fillAmount = (float)hp / maxHp;
            hpBar.color = new Color(1 - hpBar.fillAmount, hpBar.fillAmount, 0);
            if (hp <= 0)
                Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("IsDie");
        GetComponent<CapsuleCollider>().enabled = false;
        isDie = true;
        hpCanvas.enabled = false;
        audioSource.PlayOneShot(audioClip);
        G_Manager.instance.KillCount(1);
        for (int i = 0; i < weaponCollider.Length; i++)
            weaponCollider[i].enabled = false;
    }

    void HitAniEffect(Collision collision)
    {
        Destroy(collision.gameObject);
        navMeshAgent.isStopped = true;
        animator.SetTrigger("IsHit");
        GameObject bloodClone = Instantiate(bloodEffect, collision.transform.position, Quaternion.identity);
        Destroy(bloodClone, 1f);
    }
}