using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{

    public Camera mainCam; //callar kameran
    private Vector3 mousePos; //gör en mouse position

    public GameObject bullet;
    public GameObject bullet2;
    public Transform bulletTransform;
    public bool canFire;
    public bool canFire2;
    private float timer;
    private float timer2;
    public float timeBetweenFireing;
    public float timeBetweenFireing2;


    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); //gör en kortdinat och vinkel för mousepos
        Vector3 rotation = mousePos - transform.position;           
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;

            if (timer > timeBetweenFireing) //fixar så att man inte kan sjuta för snabbt
            {
                canFire = true;
                timer = 0;
            }
        }
        if (!canFire2)
        {
            timer2 += Time.deltaTime;

            if (timer2 > timeBetweenFireing2) //fixar så att man inte kan sjuta för snabbt
            {
                canFire2 = true;
                timer2 = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire) //denhär gör så att du kan spawna ett skott som sjukts iväg
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
        if (Input.GetMouseButton(1) && canFire2) //denhär gör så att du kan spawna ett skott som sjukts iväg
        {
            canFire2 = false;
            Instantiate(bullet2, bulletTransform.position, Quaternion.identity);
        }


    }
}
