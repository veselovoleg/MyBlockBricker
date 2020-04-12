using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loose : MonoBehaviour {
    // On trigger Enter 2D
    private void OnTriggerEnter2D(Collider2D collision) {
        bool ballLaunched = checkIfBallLaunched();

        if (ballLaunched) {
            Debug.Log("You loose!");
        } else {
            Debug.LogError("Ball was not launched yet");
        }
    }

    // Check if ball launched
    private bool checkIfBallLaunched() {
        Sphere sphere = FindObjectOfType<Sphere>();
        return sphere != null ? sphere.getBallLaunchedValue() : false;
    }
}
