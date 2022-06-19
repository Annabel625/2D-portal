using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float teleportTime = 0;
    public float timer = 0.2f;
    Rigidbody2D myrig;

    void Start()
    {
        myrig = GetComponent<Rigidbody2D>();
        timer = 0.2f;

    }
private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BluePortal") && teleportTime <= 0)
        {
            try
            {
                transform.position = PortalGun.BluePortal.transform.position;
                print("blue");
                teleportTime = timer;
            }
            catch
            {

            }
            
        }
        else if (collision.gameObject.CompareTag("OrangePortal") && teleportTime <= 0)
        {
            try
            {
                transform.position = PortalGun.OrangePortal.transform.position;
                print("orange");
                teleportTime = timer;
            }
            catch
            {

            }

        }
    }
    private void Update()
    {
        teleportTime -= Time.deltaTime;
        if (myrig.velocity.y>35|| myrig.velocity.y < -35)
        {
            myrig.velocity = new Vector2(myrig.velocity.x,Mathf.Clamp(myrig.velocity.y, -35, 35));
        }

    }
}
