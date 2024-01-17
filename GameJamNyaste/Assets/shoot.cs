using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{

    public Camera mainCam; //callar kameran
    private Vector3 mousePos; //g�r en mouse position

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
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); //g�r en kortdinat och vinkel f�r mousepos
        Vector3 rotation = mousePos - transform.position;           
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;

            if (timer > timeBetweenFireing) //fixar s� att man inte kan sjuta f�r snabbt
            {
                canFire = true;
                timer = 0;
            }
        }
        if (!canFire2)
        {
            timer2 += Time.deltaTime;

            if (timer2 > timeBetweenFireing2) //fixar s� att man inte kan sjuta f�r snabbt
            {
                canFire2 = true;
                timer2 = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire) //denh�r g�r s� att du kan spawna ett skott som sjukts iv�g
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
        if (Input.GetMouseButton(1) && canFire2) //denh�r g�r s� att du kan spawna ett skott som sjukts iv�g
        {
            canFire2 = false;
            Instantiate(bullet2, bulletTransform.position, Quaternion.identity);
        }


    }
}
