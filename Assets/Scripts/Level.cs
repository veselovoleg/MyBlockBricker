using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    // Options
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 0.8f;

    // On Start
    private void Start() {

    }

    // On Update
    private void Update() {
        Time.timeScale = gameSpeed;
    }
}
