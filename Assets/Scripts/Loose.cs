using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loose : MonoBehaviour {
    //State
    SceneLoader sceneLoader;

    private string currentSceneName;

    // On Start
    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // On trigger Enter 2D
    private void OnTriggerEnter2D(Collider2D collision) {
        bool ballLaunched = checkIfBallLaunched();

        if (ballLaunched) {
            if ((sceneLoader != null)) {
                sceneLoader.LoadSceneByName("Level1");
            }

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
