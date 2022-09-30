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
    Controller controller;

    const string bulletTag = "Bullet";

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

    public bool isDie { get; private set; }

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<Controller>();
    }

    void OnEnable()
    {
        isDie = false;
        hp = maxHp;
        DrawHpBar();
        GetComponent<CapsuleCollider>().enabled = true;
        hpCanvas.enabled = true;
        for (int i = 0; i < weaponCollider.Length; i++)
            weaponCollider[i].enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(bulletTag))
        {
            HitAniEffect(collision);
            hp -= 3;
            DrawHpBar();
            if (hp <= 0)
                Die();
        }
    }

    void Die()
    {
        if (isDie) return;
        isDie = true;
        animator.SetTrigger("IsDie");
        GetComponent<CapsuleCollider>().enabled = false;
        hpCanvas.enabled = false;
        audioSource.PlayOneShot(audioClip);
        G_Manager.instance.KillCount(1);
        for (int i = 0; i < weaponCollider.Length; i++)
            weaponCollider[i].enabled = false;
        Invoke("RemoveObject", 3f);
    }

    void RemoveObject()
    {
        if (tag.Equals("Monster"))
            ObjectPool.instance.ReturnMonster(controller);
        else if (tag.Equals("Zombie"))
            ObjectPool.instance.ReturnZombie(controller);
        else if (tag.Equals("Skeleton"))
            ObjectPool.instance.ReturnSkeleton(controller);
    }

    void HitAniEffect(Collision collision)
    {
        //Destroy(collision.gameObject);
        ObjectPool.instance.ReturnBullet(collision.gameObject.GetComponent<BulletCtrl>());
        navMeshAgent.isStopped = true;
        animator.SetTrigger("IsHit");
        GameObject bloodClone = Instantiate(bloodEffect, collision.transform.position, Quaternion.identity);
        Destroy(bloodClone, 1f);
    }

    void DrawHpBar()
    {
        hpBar.fillAmount = (float)hp / maxHp;
        hpBar.color = new Color(1 - hpBar.fillAmount, hpBar.fillAmount, 0);
    }
}