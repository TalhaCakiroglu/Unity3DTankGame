using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    string moveAxisName = "Vertical";
    string turnAxisName = "Horizontal";
    float moveSpeed = 10f;
    float turnSpeed = 240f;

    float moveAxis;
    float turnAxis;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb = gameObject.AddComponent<Rigidbody>();  //üstteki ile aymı işlemi yapar farkı rigidbody yok ise oyun başlar başlamaz oluşurur
    }
    void Update()
    {
        moveAxis = Input.GetAxis(moveAxisName);
        turnAxis = Input.GetAxis(turnAxisName);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetComponent<ShootBehaviour>().Shoot();
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * moveAxis * moveSpeed * Time.deltaTime); //fiziksel hareketleri burada yapıyoruz
        Quaternion rotation = Quaternion.Euler(0, turnAxis * turnSpeed * Time.deltaTime, 0);
        rb.MoveRotation(transform.rotation * rotation);
    }
}
