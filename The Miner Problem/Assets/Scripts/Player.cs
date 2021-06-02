using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton

	public static Player instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}

	#endregion

    // Characteristics
    private float speed = 2.5f;
    private float jumpForce = 4.0f;

    // Movement
    public int maxJumps = 2;
    public int jumpCounter = 2;
    public bool isGrounded;

    // Components
    private Rigidbody2D rigidBody;
    private Animator animator;

    // Canvas
    private Color frozenScore = new Color (0.21f, 0.90f, 1.0f, 1.0f);
    private Color activeScore = new Color (1.0f, 0.86f, 0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        updateAnimator();
        handleScore();
        handleSuicide();
    }

    private void updateAnimator()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetFloat("velocityY", rigidBody.velocity.y);
    }

    private void handleSuicide()
    {
        if (gameObject.transform.position.y <= -6f)
            GameManager.instance.handleGameOver();
    }

    private void handleScore ()
    {
        if (Mathf.Abs(rigidBody.velocity.x) < 0.1 && Mathf.Abs(rigidBody.velocity.y ) < 0.1) {
            GameManager.instance.score += Time.deltaTime;
            CanvasManager.instance.scoreLabel.color = activeScore;
        }
        else {
            CanvasManager.instance.scoreLabel.color = frozenScore;
        }
    }

    private void move() 
    {
        moveHorizontally();

        if(Input.GetKeyDown("space") && jumpCounter > 0)
            jump();
    }

    private void moveHorizontally ()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(direction * speed, rigidBody.velocity.y);
    }

    private void jump ()
    {
        rigidBody.velocity = Vector2.up * jumpForce;
        jumpCounter--;
    }
}
