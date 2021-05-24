using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : MonoBehaviour
{
    public CircleCollider2D circleCollider;

    public float speed;

    private Vector2 targetPosition;    

    // Start is called before the first frame update
    void Start()
    {
        defineTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        handleObjectLife();
        move();
    }

    private void defineTargetPosition () 
    {
        targetPosition = Player.instance.transform.position;

        Vector2 myPosition = gameObject.transform.position;

        // Goes 10 times the distance between my position and player position (parametric line equation).
        targetPosition.x = 10 * (targetPosition.x - myPosition.x) + myPosition.x;
        targetPosition.y = 10 * (targetPosition.y - myPosition.y) + myPosition.y;
    }

    private float distanceFromPlayer ()
    {
        return Vector3.Distance(transform.position, Player.instance.transform.position);
    }

    private void handleObjectLife ()
    {
        if(distanceFromPlayer() > 12.0)
            Destroy(gameObject);
    }

    private void move ()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.tag == "Player")
            GameManager.instance.handleGameOver();
    }
}
