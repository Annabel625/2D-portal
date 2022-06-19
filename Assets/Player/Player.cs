using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpheight=5;
    public float acc = 2;
    public float maxspeed = 5;

    Rigidbody2D myrig;

    public bool isGround;
    public bool isLWall;
    public bool isRWall;
    public bool isfall;

    public Transform gchecker;
    public Transform lwchecker;
    public Transform rwchecker;

    //float checkRadius = 0.2f;
    public LayerMask GroundLayer;

    public SpriteRenderer currentFace;
    public Sprite neutralFace;
    public Sprite jumpFace;
    public Sprite leftFace;
    public Sprite rightFace;
    public Sprite fallFace;
    // Start is called before the first frame update
    void Start()
    {
        myrig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapBox(gchecker.position,new Vector2(1f,0.2f),0,GroundLayer);
        isLWall = Physics2D.OverlapBox(lwchecker.position, new Vector2(0.8f, 0.1f),90, GroundLayer);
        isRWall = Physics2D.OverlapBox(rwchecker.position, new Vector2(0.8f, 0.1f),90, GroundLayer);
        isfall = (myrig.velocity.y < 0.0);


        float face = Input.GetAxisRaw("Horizontal");
        myrig.AddForce(new Vector2(acc * face, 0)); // Horizontal movement

        
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && (isLWall || isRWall)) //Wall jump
        {
            myrig.velocity = new Vector2(myrig.velocity.x, 0);
            myrig.AddForce(new Vector2(acc * -face * 3, jumpheight), ForceMode2D.Impulse);
        }else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && (isGround)) //Ground jump
        {
            myrig.AddForce(new Vector2(0, jumpheight), ForceMode2D.Impulse); ;
        }
        if(myrig.velocity.x >= maxspeed || myrig.velocity.x <= -maxspeed) //maxspeed
        {
            myrig.velocity = new Vector2(Mathf.Clamp(myrig.velocity.x, -maxspeed, maxspeed),myrig.velocity.y);
        }

        switch (face)
        {
            case 0:
                myrig.velocity = new Vector2(0, myrig.velocity.y);
                currentFace.sprite = neutralFace;
                if (!isGround)
                {
                    if (isfall)
                    {
                        currentFace.sprite = fallFace;
                    }
                    else
                    {
                        currentFace.sprite = jumpFace;
                    }
                }
                break;
            case 1:
                currentFace.sprite = rightFace;
                break;
            case -1:
                currentFace.sprite = leftFace;
                break;
        }



    }

    private void FixedUpdate()
    {

    }

}
