using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); //dethär gör så att bulleten följer riktningen
                                                                                         //av musen när man klickar och sen fortsätter 
                                                                                         // i en bestämd fart.
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            //ta bort liv från enemy
            Destroy(gameObject);
        }
    }

        // Update is called once per frame
        void Update()
        {
        
        }
}
