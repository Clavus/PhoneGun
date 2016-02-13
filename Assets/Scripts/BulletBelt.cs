using UnityEngine;
using System.Collections;

public class BulletBelt : MonoBehaviour
{

    public int maxBullets = 10;

    [SerializeField]
    private GameObject bulletObject;

    private int bulletsLeft = 0;
    private GameObject[] bullets;

    void Start ()
    {
        bullets = new GameObject[maxBullets];
        Reload();
    }

    public void UseBullet()
    {
        var rigid = bullets[bulletsLeft-1].GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.None;
        rigid.AddForce(Vector3.up * Random.Range(2f, 3f) + Vector3.forward * (0.2f + Random.value) + Vector3.right * (-0.1f + 0.2f * Random.value), ForceMode.Impulse);
        rigid.AddTorque(new Vector3(-1 + 2 * Random.value, -1 + 2 * Random.value, -1 + 2 * Random.value), ForceMode.Impulse);
        var pool = bullets[bulletsLeft - 1].AddComponent<PoolAfter>();
        pool.seconds = 2f;
        bulletsLeft--;
    }

    public int GetBulletsLeft()
    {
        return bulletsLeft;
    }

    public void Reload()
    {
        if (bulletsLeft == maxBullets)
            return;

        for (int i = bulletsLeft; i < maxBullets; i++)
        {
            var obj = ObjectPool.Get(bulletObject, Vector3.zero, Quaternion.identity);
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(i * 0.075f, 0, (i % 2) * 0.03f);
            obj.transform.localEulerAngles = new Vector3(0,90,0);
            var rigid = obj.GetComponent<Rigidbody>();
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            bullets[i] = obj;
        }

        bulletsLeft = maxBullets;
    }

}
