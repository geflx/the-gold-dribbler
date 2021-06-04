using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class Jewel : MonoBehaviour
{
    public CircleCollider2D circleCollider;

    public float speed;

    private Vector2 targetPosition;  
    private Light2D light;

    private float minOuterRadius = 1.3f;
    private float maxOuterRadius = 2.3f;
    private bool increaseOuterRadius = true;

    private float minIntensity = 1f;
    private float maxIntensity = 2f;
    private bool increaseIntensity = true;

    // Start is called before the first frame update
    void Start()
    {
        defineTargetPosition();
        
        light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        changeOuterRadius();
        changeIntensity();
        handleObjectLife();
        move();
    }

    private void changeOuterRadius ()
    {
        if (increaseOuterRadius)
            light.pointLightOuterRadius += (speed / 3.0f) * Time.deltaTime;
        else
            light.pointLightOuterRadius -= (speed / 3.0f) * Time.deltaTime;
        
        // Check boundaries (and change bool guide variable if necessary).
        if(light.pointLightOuterRadius > maxOuterRadius || light.pointLightOuterRadius < minOuterRadius) {
            increaseOuterRadius = !increaseOuterRadius;
        }
    }

    private void changeIntensity ()
    {
        if (increaseIntensity)
            light.intensity += (speed / 2.0f) * Time.deltaTime;
        else
            light.intensity -= (speed / 2.0f) * Time.deltaTime;
        
        // Check boundaries (and change bool guide variable if necessary).
        if(light.intensity > maxIntensity || light.intensity < minIntensity) {
            increaseIntensity = !increaseIntensity;
        }
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
        if(distanceFromPlayer() > 20.0 || gameObject.transform.position.y > 6.0f)
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
