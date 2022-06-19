using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject portalBulletPrefab;
    public int bounceTime;
    public int bounceLimit = 5;

    public Transform GunPoint;

    Vector2 playerPosition;
    Vector2 mousePosition;

    public static GameObject BluePortal;
    public static GameObject OrangePortal;



    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            playerPosition = player.transform.position;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            FirePortal(mousePosition, bounceTime, false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            playerPosition = player.transform.position;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            FirePortal(mousePosition, bounceTime, true);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (bounceTime < bounceLimit)
            {
                bounceTime++;
            }
        }
    }

    void FirePortal(Vector2 mousePos, int _bounceTime, bool _isOrange)
    {
        GameObject portalBullet = Instantiate(portalBulletPrefab, GunPoint.position, new Quaternion(0,0,0,0));
        portalBullet.GetComponent<PortalBullet>().gunPoint = GunPoint.position;
        portalBullet.GetComponent<PortalBullet>().target = mousePos;
        portalBullet.GetComponent<PortalBullet>().bounceTime = _bounceTime;
        portalBullet.GetComponent<PortalBullet>().isOrange = _isOrange;

        bounceTime = 0;
    }
}
