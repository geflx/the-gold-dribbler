using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* This class represents a horizontal FPS joystick control */

public class FPSJoystick : MonoBehaviour {
    
    private Vector2 startTouchPos, touchDirection;

    public Image analog;
    public Image backgroundLine;

    public int horizontal;
    public float handleRange;
    public float analogBorderDistance;
    private Vector2 analogStartPos, analogPos;

    void Start() {
        analogPos = analogStartPos = analog.transform.position;
    }

    void Update() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase) {

                case TouchPhase.Began:
                    startTouchPos = touch.position;
                    analogPos = analogStartPos;
                    break;
                
                case TouchPhase.Moved:
                    touchDirection = touch.position - startTouchPos;
                    analogPos = analogStartPos;

                    if (touchDirection.x < handleRange) {
                        horizontal = -1;
                        analogPos.x -= analogBorderDistance;
                    } else if (touchDirection.x > handleRange) {
                        horizontal = 1;
                        analogPos.x += analogBorderDistance;
                    }
                    else {
                        horizontal = 0;
                    }

                    break;

                case TouchPhase.Ended:
                    analogPos = analogStartPos;
                    horizontal = 0;
                    break;
            }
        }

        analog.transform.position = analogPos;
    }

}
