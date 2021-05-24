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

    // Components
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move() 
    {
        moveHorizontally();

        if(Input.GetKeyDown("space") && jumpCounter > 0)
            jump();
    }

    void moveHorizontally ()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(direction * speed, rigidBody.velocity.y);
    }

    void jump ()
    {
        rigidBody.velocity = Vector2.up * jumpForce;
        jumpCounter--;
    }
}
