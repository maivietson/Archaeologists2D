using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] GameObject panelNPCs;
    [SerializeField] Text txtNPCs;

    // Panel Option
    // [SerializeField] GameObject panelOptions;

    // Cached component references
    private Animator myAnimator;
    private Rigidbody2D myRigibody;
    private CapsuleCollider2D myBodyCollider2D;
    private BoxCollider2D myFeetCollider2D;
    private float gravityScaleAtStart;

    // State
    private bool isAlive = true;
    private bool isStartCollider = false;
    private bool isEndCollider = false;

    // handle time count
    private int timeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigibody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) { return; }
        Run();
        ClimbLadder();
        Jump();
        Die();
        FlipSprite();
        CountTime();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SpawnStart" && !isStartCollider)
        {
            txtNPCs.text = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_START_COLLIDER_TRAPS);
            panelNPCs.SetActive(true);
            ObstacleSpawner[] gameObjects = FindObjectsOfType<ObstacleSpawner>();
            //print("collision");
            isStartCollider = true;
            isEndCollider = false;
            foreach(ObstacleSpawner ob in gameObjects)
            {
                ob.isSpawner = true;
                ob.startSpawning = true;
            }
        }

        if(collision.gameObject.tag == "SpawnEnd" && !isEndCollider)
        {
            txtNPCs.text = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_END_COLLIDER_TRAPS);
            panelNPCs.SetActive(true);
            ObstacleSpawner[] gameObjects = FindObjectsOfType<ObstacleSpawner>();
            isStartCollider = false;
            isEndCollider = true;
            foreach (ObstacleSpawner ob in gameObjects)
            {
                ob.isSpawner = false;
                ob.startSpawning = false;
            }
        }

        if(collision.gameObject.tag == "GateStart" && !isStartCollider)
        {
            txtNPCs.text = Texts.instance.GetText(Types.NPCs.Type, Types.NPCs.ID.STR_IMFORMATION_6);
            panelNPCs.SetActive(true);
            isStartCollider = true;
        }
    }

    private void CountTime()
    {
        if(isStartCollider || isEndCollider) ++timeCount;
        if(timeCount == 600)
        {
            panelNPCs.SetActive(false);
            timeCount = 0;
        }
    }

    public void onClickButton()
    {
        panelNPCs.SetActive(false);
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigibody.velocity.y);
        myRigibody.velocity = playerVelocity;

        // enable animation run
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigibody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void ClimbLadder()
    {
        if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigibody.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigibody.velocity.x, controlThrow * climbSpeed);
        myRigibody.velocity = climbVelocity;
        myRigibody.gravityScale = 0f;

        // enable animation climb
        bool playerHasVerticalSpeed = Mathf.Abs(myRigibody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void Jump()
    {
        // if player not yet touch ground then no jump agian
        if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if(CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigibody.velocity += jumpVelocityToAdd;
        }
    }

    private void Die()
    {
        if(myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards", "FireObtacles", "HazardOb")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigibody.velocity.x) > Mathf.Epsilon;
        {
            //print("myRigibody.velocity.x: " + myRigibody.velocity.x);
            //print("Epsilon: " + Mathf.Epsilon);
            // reverse tbe current scalling of x exis
            if(playerHasHorizontalSpeed)
            {
                transform.localScale = new Vector2(Mathf.Sign(myRigibody.velocity.x), 1f);
            }
        }
    }
}
