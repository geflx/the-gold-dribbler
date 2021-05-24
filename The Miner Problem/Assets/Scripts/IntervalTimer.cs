using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalTimer : MonoBehaviour
{
    private float duration = 4.0f;

    void Start()
    {
        HordeManager.instance.intervalOn = true;
    }

    void Update()
    {
        runClock();

        if (duration < 0.0f)
            OnTimeIsUp();
    }

    private void runClock ()
    {
        duration -= Time.deltaTime;
    }

    private void OnTimeIsUp ()
    {
        HordeManager.instance.intervalOn = false;

        /* show message about next Horde in Game UI */

        /* maybe this timer will need to talk with horde timer... */
    }
}
