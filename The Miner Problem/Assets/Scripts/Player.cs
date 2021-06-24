using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton
	public static Player instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of Player found!");
			return;
		}
		instance = this;
	}
	#endregion

    // Characteristics
    public float defaultSpeed = 3.2f;
    public float speed = 3.2f;
    private float jumpForce = 4.9f;

    // Movement
    public int maxJumps = 2;
    public int jumpCounter = 2;
    public bool isGrounded = true;
    private float direction = -1f;
    private float lastHorizontalDir = -1f;

    // Components
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Canvas
    private Color frozenScore = new Color (0.21f, 0.90f, 1.0f, 1.0f);
    private Color activeScore = new Color (1.0f, 0.86f, 0f, 1.0f);

    // Mobile mode
    public FPSJoystick fpsJoystick;
    public Joystick joystick;
    public bool mobileMode;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        checkSpriteRendererFlip();
        updateAnimator();
        updateScore();
        handleDeath();
    }

    // Flip sprite renderer horizontally
    private void checkSpriteRendererFlip()
    {
        if (direction == 0f)
            return;
            
        if (direction != lastHorizontalDir) {        
            spriteRenderer.flipX = !spriteRenderer.flipX;
            lastHorizontalDir = direction;
        }
    }

    // Updating animator variables
    private void updateAnimator()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetFloat("velocityY", rigidBody.velocity.y);
    }

    // Checking player positionY and maybe defining gameOver
    private void handleDeath()
    {
        if (gameObject.transform.position.y <= -6f)
            GameManager.instance.handleGameOver();
    }

    // Update score and its text color
    private void updateScore ()
    {
        if (HordeManager.instance.runScore) {
            GameManager.instance.score += Time.deltaTime;
            CanvasManager.instance.scoreLabel.color = activeScore;
        }
        else {
            CanvasManager.instance.scoreLabel.color = frozenScore;
        }
    }

    private void move()
    {
        if (mobileMode)
            moveMobileMode();
        else
            moveDesktopMode();
    }

    // (Desktop) Move using keyboard
    private void moveDesktopMode() 
    {
        direction = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(direction * speed, rigidBody.velocity.y);

        if(Input.GetKeyDown("space") && jumpCounter > 0)
            jump();
    }

    private void jump ()
    {
        rigidBody.velocity = Vector2.up * jumpForce;
        jumpCounter--;
    }

    // (Mobile) Move using joystick
    private void moveMobileMode() 
    {
        // Handle joystick dead zone sensitivity.
        if (joystick.Horizontal >= 0.2f)
            direction = 1;
        else if (joystick.Horizontal <= -0.2f)
            direction = -1;
        else
            direction = 0;

        //direction = fpsJoystick.horizontal; // Future update!

        rigidBody.velocity = new Vector2(direction * speed, rigidBody.velocity.y);
    }

    // (Mobile) Jump function (called by jump button)
    public void jumpMobileMode()
    {
        if(jumpCounter > 0)
            jump();
    }
}
