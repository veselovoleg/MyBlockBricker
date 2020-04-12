using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    // Options
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 0.8f;
    [SerializeField] bool showMouse = true;

    // On Start
    private void Start() {
        // Set cursor visibility
        Cursor.visible = showMouse;
    }

    // On Update
    private void Update() {
        Time.timeScale = gameSpeed;
    }
}
