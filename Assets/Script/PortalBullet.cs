using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PortalBullet : MonoBehaviour
{
    public Vector2 bulletpos;
    public Vector2 bulletdir;
    private Rigidbody2D rb;
    public float speed = 15f;
    public Vector2 target;
    public Vector2 lastVelocity;
    public int bounceTime = 0;
    public GameObject PortalOrange;
    public GameObject PortalBlue;
    public bool isOrange;
    public Vector2 gunPoint;
    public float selfDestructTime = 5f;
    public Grid wallgrid;
    public Tilemap walltile;
    public Tile orangePortalTile;
    public GameObject bullet;
    static Vector3 orangePortalPosition;
    static Vector3 bluePortalPosition;
    public Ray bulletray;
    public int c = 0;
    public RaycastHit2D hitdata;
    public RaycastHit2D hitstore0;
    public RaycastHit2D hitstore1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        walltile = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    }
    void Start()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        rb.velocity = (target - gunPoint).normalized * speed;
        Destroy(gameObject, selfDestructTime);

    }

    void FixedUpdate()
    {
        bulletpos = rb.transform.position;
        lastVelocity = rb.velocity;
        bulletdir = new Vector2(rb.velocity.x, rb.velocity.y);
        bulletray = new Ray(bulletpos, bulletdir);



        hitdata = Physics2D.Raycast(bulletpos, bulletdir);
        Debug.DrawRay(bulletpos, bulletdir);
        if (hitdata.collider != null)
            if (c == 0)
            {
                hitstore0 = hitdata;
                c++;
            }
            else
            {
                hitstore1 = hitdata;
                c--;
            }



        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 6 && bounceTime > 0)
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
            bounceTime--;
        }
        else if (collision.gameObject.CompareTag("Portalable") && bounceTime <= 0)
        {
            if (isOrange)
            {
                if (PortalGun.OrangePortal != null)
                {
                    Destroy(PortalGun.OrangePortal);
                }
                Vector2 hitPosition = Vector2.zero;
                if (c == 0)
                {
                    hitPosition.x = hitstore1.point.x - 0.01f * hitstore0.normal.x;
                    hitPosition.y = hitstore1.point.y - 0.01f * hitstore0.normal.y;
                    var x = hitstore1.normal.x;
                    var y = hitstore1.normal.y;

                    print("norm" + hitstore0.normal);
                    print("pos" + hitstore1.point);
                }
                else
                {
                    hitPosition.x = hitstore0.point.x - 0.01f * hitstore1.normal.x;
                    hitPosition.y = hitstore0.point.y - 0.01f * hitstore1.normal.y;
                    var x = hitstore0.normal.x;
                    var y = hitstore0.normal.y;

                    print("norm" + hitstore1.normal);
                    print("pos" + hitstore0.point);
                }

                /*
                Vector3 hitPosition = Vector3.zero;

                hitPosition.x = collision.contacts[0].point.x - 0.01f * collision.contacts[0].normal.x;
                hitPosition.y = collision.contacts[0].point.y - 0.01f * collision.contacts[0].normal.y;

                //print("point"+ collision.contacts[0].point);
                print("normal" + collision.contacts[0].normal);

                var x = collision.contacts[0].normal.x;
                var y = collision.contacts[0].normal.y;

                if (y > 0.5f)
                {
                    hitPosition.y += 1;
                }
                else if (y < -0.5f)
                {
                    hitPosition.y -= 1;
                }
                if (x > 0.5f)
                {
                    hitPosition.x += 1;
                }
                else if (x < -0.5f)
                {
                    hitPosition.x -= 1;
                }
                */



                /*
                switch ((collision.contacts[0].normal.x, collision.contacts[0].normal.y))
                {
                    case (0, > 0.5f):
                        {
                            hitPosition.y += 1;
                            break;
                        }
                    case (0, < 0.5f):
                        {
                            hitPosition.y -= 1;
                            break;
                        }
                    case ( > 0.5f, 0):
                        {
                            hitPosition.x += 1;
                            break;
                        }
                    case ( < 0.5f, 0):
                        {
                            hitPosition.x -= 1;
                            break;
                        }
                }
                */
                if (orangePortalPosition != null)
                {
                    walltile.SetTile(walltile.WorldToCell(orangePortalPosition), null);
                    //print("before: "+ orangePortalPosition);
                }
                //print("before: " + orangePortalPosition);
                orangePortalPosition = hitPosition;
                walltile.SetTile(walltile.WorldToCell(hitPosition), orangePortalTile);
                //print("after: " + orangePortalPosition);


                //print("Normal " + hitstore0.normal);
                //print("Point " +hit.point);



                PortalGun.OrangePortal = Instantiate(PortalOrange, collision.contacts[0].point, Quaternion.identity);
            }
            else
            {
                if (PortalGun.BluePortal != null)
                {
                    Destroy(PortalGun.BluePortal);
                }
                PortalGun.BluePortal = Instantiate(PortalBlue, collision.contacts[0].point, Quaternion.identity);
            }
            Destroy(gameObject);
        }

    }

}
