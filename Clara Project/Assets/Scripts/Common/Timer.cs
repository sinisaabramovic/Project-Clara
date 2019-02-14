using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour {

    public float currCountdownValue;
    public bool canExit = true;

    public IEnumerator StartCountdown(Action<bool> onComplete, float countdownValue = 10)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        if(onComplete != null)
        {
            onComplete(true);
        }
    }
}
