using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;

    Queue<BulletCtrl> bulletPool = new Queue<BulletCtrl>();
    Queue<Controller> monsterPool = new Queue<Controller>();
    Queue<Controller> zombiePool = new Queue<Controller>();
    Queue<Controller> skeletonPool = new Queue<Controller>();

    [Header("About Player")]
    [SerializeField]
    BulletCtrl bulletPrefab;

    [Header("About Enemies")]
    [SerializeField]
    Controller monsterPrefab;
    [SerializeField]
    Controller zombiePrefab;
    [SerializeField]
    Controller skeletonPrefab;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Return deactivated bullet
    /// </summary>
    /// <returns></returns>
    public BulletCtrl GetBullet()
    {
        if (bulletPool.Count == 0)
        {
            BulletCtrl bulletClone = Instantiate(bulletPrefab);
            bulletClone.gameObject.SetActive(false);
            return bulletClone;
        }
        return bulletPool.Dequeue();
    }

    public void ReturnBullet(BulletCtrl bullet)
    {
        if (bullet.gameObject == null) return;
        if (bullet.gameObject.activeSelf)
            bullet.gameObject.SetActive(false);
        bullet.Clear();
        bulletPool.Enqueue(bullet);
    }

    public Controller GetMonster()
    {
        if (monsterPool.Count == 0)
        {
            Controller monsterClone = Instantiate(monsterPrefab);
            monsterClone.gameObject.SetActive(false);
            return monsterClone;
        }
        return monsterPool.Dequeue();
    }

    public void ReturnMonster(Controller monster)
    {
        if (monster.gameObject == null) return;
        if (!monster.tag.Equals("Monster"))
        {
            Debug.Log("Monster tag incorrect!");
            return;
        }
        if (monster.gameObject.activeSelf)
            monster.gameObject.SetActive(false);
        monster.Clear();
        monsterPool.Enqueue(monster);
    }

    public Controller GetZombie()
    {
        if (zombiePool.Count == 0)
        {
            Controller zombieClone = Instantiate(zombiePrefab);
            zombieClone.gameObject.SetActive(false);
            return zombieClone;
        }
        return zombiePool.Dequeue();
    }

    public void ReturnZombie(Controller zombie)
    {
        if (zombie.gameObject == null) return;
        if (!zombie.tag.Equals("Zombie"))
        {
            Debug.Log("Zombie tag incorrect!");
            return;
        }
        if (zombie.gameObject.activeSelf)
            zombie.gameObject.SetActive(false);
        zombie.Clear();
        zombiePool.Enqueue(zombie);
    }

    public Controller GetSkeleton()
    {
        if (skeletonPool.Count == 0)
        {
            Controller skeletonClone = Instantiate(skeletonPrefab);
            skeletonClone.gameObject.SetActive(false);
            return skeletonClone;
        }
        return skeletonPool.Dequeue();
    }

    public void ReturnSkeleton(Controller skeleton)
    {
        if (skeleton.gameObject == null) return;
        if (!skeleton.tag.Equals("Skeleton"))
        {
            Debug.Log("Skeleton tag incorrect!");
            return;
        }
        if (skeleton.gameObject.activeSelf)
            skeleton.gameObject.SetActive(false);
        skeleton.Clear();
        skeletonPool.Enqueue(skeleton);
    }

    public void DestroyAll()
    {
        while (bulletPool.Count != 0)
        {
            var obj = bulletPool.Dequeue();
            obj.Clear();
            Destroy(obj.gameObject);
        }
        while (monsterPool.Count != 0)
        {
            var obj = monsterPool.Dequeue();
            obj.Clear();
            Destroy(obj.gameObject);
        }
        while (zombiePool.Count != 0)
        {
            var obj = zombiePool.Dequeue();
            obj.Clear();
            Destroy(obj.gameObject);
        }
        while (skeletonPool.Count != 0)
        {
            var obj = skeletonPool.Dequeue();
            obj.Clear();
            Destroy(obj.gameObject);
        }
    }
}
