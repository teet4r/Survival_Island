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
    public Transform[] points;
    public GameObject zombiePrefab;
    public GameObject monsterPrefab;
    public GameObject skeletonPrefab;
    public int total { get; private set; } = 0;
    public static G_Manager instance = null;

    [SerializeField]
    [Range(0.1f, 10f)]
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
                CreateZombie();
            if (GameObject.FindGameObjectsWithTag("Monster").Length < monsterMaxCnt)
                CreateMonster();
            if (GameObject.FindGameObjectsWithTag("Skeleton").Length < skeletonMaxCnt)
                CreateSkeleton();
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
