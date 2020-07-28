using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //yapay zeka kütüphanesi

public class EnemyAI : MonoBehaviour
{
    Transform player;
    NavMeshAgent agent;
    public Transform[] wayPoints; //duraklar
    public Transform rayOrigin;
    int currentWayPointIndex=0;
    float shootFrekans = 5f;
    Animator fsm;
    Vector3[] wayPointPos = new Vector3[3]; //durakların pozisyonu

    void Start()
    {
        for (int i = 0; i < wayPoints.Length; i++) //waypoints dizisinin uzunluğu
        {
            wayPointPos[i] = wayPoints[i].position; //durakların pozisyonunu kaydetme
        }

        fsm = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wayPointPos[currentWayPointIndex]); //istediğimiz hedefe gitmesini sağlıyoruz.

        StartCoroutine("CheckPlayer"); //check player metodunu çalıştırır
    }

    IEnumerator CheckPlayer() //bu metot raycast ı saniyede istediğimiz kadar yollamızı sağlar. update,late,fixed bunlar saniyede 50-60 defa çağırdığı için oyunu yorar.
    {
        CheckVisibiility();
        CheckDistance();

        yield return new WaitForSeconds(0.1f); // saniyenin 10 da 1 i bekle
        yield return CheckPlayer(); //update metodu gibi çalışır
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(player.position, rayOrigin.position); //2.yöntem
        //  (player.position - transform.position).magnitude; //player ile tank arasındaki mesafeyi bulma 1.yöntem

        fsm.SetFloat("Distance", distance);
    }

    private void CheckVisibiility()
    {
        float maxDistance = 20;

        //ilk önce yön vektörü hesaplanmalı (tank tan player a ışın çizmek için
        Vector3 direction = (player.position - rayOrigin.position).normalized; //ışının boyutunu kontrol etmek için normarlize ediliyor.(kendine bölünüp 1 birim elde edilir)
        //Vector3 direction2 = (player.position-transform.position) / (player.position-transform.position).magnitude; // bu iki satır aynı işi yapar.
        float distanceFromWayPoint = Vector3.Distance(transform.position, wayPointPos[currentWayPointIndex]);
        fsm.SetFloat("DistanceFromWayPoint", distanceFromWayPoint);
        Debug.DrawRay(rayOrigin.position, direction * maxDistance, Color.red);

        if (Physics.Raycast(rayOrigin.position, direction, out RaycastHit info,maxDistance))
        {
            if (info.transform.tag == "Player")
            {
                fsm.SetBool("isVisible",true);
            }
            else
            {
                fsm.SetBool("isVisible", false);
            }
        }
        else
        {
            fsm.SetBool("isVisible", false);
        }
    }

    public void Shoot() //ates fsm çalışır
    {
        GetComponent<ShootBehaviour>().Shoot(shootFrekans);
    }

    public void Chase() //takip fsm çalışır
    {
        agent.SetDestination(player.position);
    }

    public void Patrol() //devriye fsm çalışır
    {
        //Debug.Log("Patrolling");
    }

    public void SetLookRotation()
    {
        Vector3 dir = (player.position - transform.position).normalized;//birim yön vektörü oluşturduk
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation,rotation,0.2f);//interpolasyon yaparak bir smooting oluşturarak tankın daha yumuşak dönmesini sağlar
    }

    public void SetNewWayPoint()
    {
        switch (currentWayPointIndex)
        {
            case 0:
                currentWayPointIndex = 1;
                break;
            case 1:
                currentWayPointIndex = 2;
                break;
            case 2:
                currentWayPointIndex = 0;
                break;
        }
        agent.SetDestination(wayPointPos[currentWayPointIndex]);
    }
}
