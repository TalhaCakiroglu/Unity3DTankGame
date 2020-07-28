using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    float forceAmount = 1200f;
    float timeFromLastShoot = 0;
    Rigidbody rb;

    private void Start()
    {
        rb = bulletPrefab.GetComponent<Rigidbody>();
    }
    public void Shoot(float shootFrekans)
    {
        if ((timeFromLastShoot += Time.deltaTime) >= 1f/shootFrekans)
        {
            InstantiateBullet();
            timeFromLastShoot = 0;
        }
    }
    public void Shoot()
    {
        InstantiateBullet();   
       
    }
    private void InstantiateBullet()
    {
        Rigidbody bullet = Instantiate(rb, bulletSpawn.position, Quaternion.identity); //birim rotasyon etkisi yok yazmamızın nedeni metot da 3. parametreyi boş bırakamadığımız için kullandık
        bullet.AddForce(forceAmount * transform.forward);
    }
}
