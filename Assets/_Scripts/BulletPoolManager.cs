using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!

[System.Serializable]
public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance { get; private set; }
    public GameObject bullet;
    public int maxBullets = 0;

    //TODO: create a structure to contain a collection of bullets
    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: add a series of bullets to the Bullet Pool
        _BuildBulletPool();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void _BuildBulletPool()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            bulletPool.Enqueue(Instantiate(bullet));
            for (int j = 0; j < i; j++)
            {
                bulletPool.Enqueue(bulletPool.Dequeue());
            }
            bulletPool.Peek().transform.parent = this.transform;
            bulletPool.Peek().SetActive(false);
        }
    }

    //TODO: modify this function to return a bullet from the Pool
    public GameObject GetBullet(Vector3 pos)
    {
        if (bulletPool.Count <= 0)
        {
            //bulletPool.Enqueue(Instantiate(bullet));
            ResetBullet(bullet);
        }

        bulletPool.Peek().SetActive(true);
        bulletPool.Peek().transform.position = pos;
        bulletPool.Peek().transform.localRotation = Quaternion.identity;
        return bulletPool.Dequeue();
    }

    //TODO: modify this function to reset/return a bullet back to the Pool 
    public void ResetBullet(GameObject bullet)
    {
        bulletPool.Enqueue(bullet);
        bullet.SetActive(false);
    }

    public int BulletPoolSize()
    {
        return bulletPool.Count;
    }

    public bool IsBulletPoolEmpty()
    {
        if(BulletPoolSize() <= 0)
        {
            return true;
        }

        return false;
    }
}
