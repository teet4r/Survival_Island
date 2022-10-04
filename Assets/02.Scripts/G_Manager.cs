using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 적 생성 로직
/// 1. 태어날 위치
/// 2. 좀비 프리팹
/// 3. 몇 초 간격으로 스폰?
/// 4. 몇 마리 스폰?
/// </summary>
public class G_Manager : MonoBehaviour
{
    public static G_Manager instance = null;

    public Transform[] points;
    public GameObject zombiePrefab;
    public GameObject monsterPrefab;
    public GameObject skeletonPrefab;
    public int total { get; private set; } = 0;

    [SerializeField][Range(0.1f, 10f)]
    float responTime;
    float timePrev;
    [SerializeField]
    Text killText;
    
    [SerializeField]
    int zombieMaxCnt;
    [SerializeField]
    int monsterMaxCnt;
    [SerializeField]
    int skeletonMaxCnt;

    void Start()
    {
        instance = this;
        timePrev = Time.time;
    }

    void Update()
    {
        if (Time.time - timePrev > responTime)
        {
            timePrev = Time.time;
            if (GameObject.FindGameObjectsWithTag("Zombie").Length < zombieMaxCnt)
            {
                int idx = Random.Range(0, points.Length);
                var zombie = ObjectPool.instance.GetZombie();
                zombie.transform.position = points[idx].position;
                zombie.transform.rotation = points[idx].rotation;
                zombie.gameObject.SetActive(true);
            }
            if (GameObject.FindGameObjectsWithTag("Monster").Length < monsterMaxCnt)
            {
                int idx = Random.Range(0, points.Length);
                var monster = ObjectPool.instance.GetMonster();
                monster.transform.position = points[idx].position;
                monster.transform.rotation = points[idx].rotation;
                monster.gameObject.SetActive(true);
            }
            if (GameObject.FindGameObjectsWithTag("Skeleton").Length < skeletonMaxCnt)
            {
                int idx = Random.Range(0, points.Length);
                var skeleton = ObjectPool.instance.GetSkeleton();
                skeleton.transform.position = points[idx].position;
                skeleton.transform.rotation = points[idx].rotation;
                skeleton.gameObject.SetActive(true);
            }
        }
    }

    void CreateZombie()
    {
        int idx = Random.Range(0, points.Length);
        Instantiate(zombiePrefab, points[idx].position, points[idx].rotation);
    }

    void CreateMonster()
    {
        int idx = Random.Range(0, points.Length);
        Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);
    }

    void CreateSkeleton()
    {
        int idx = Random.Range(0, points.Length);
        Instantiate(skeletonPrefab, points[idx].position, points[idx].rotation);
    }

    public void KillCount(int count)
    {
        total += count;
        killText.text = "Kills: " + "<color=#ff0000>" + total.ToString() + "</color>";
    }
}
