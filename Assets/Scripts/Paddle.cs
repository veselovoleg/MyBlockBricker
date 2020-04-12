using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    // Options
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float screenWidthInUnits;

    // State
    GameStatus gameStatus;
    Sphere sphere;

    // On Start
    private void Start() {
        gameStatus = FindObjectOfType<GameStatus>();
        sphere = FindObjectOfType<Sphere>();
    }

    // On Update 
    private void Update() {
        CalculatePaddlePosition();
    }

    // Calcualte paddle position on Ox
    private void CalculatePaddlePosition() {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetPositionOx(), minX, maxX);
        transform.position = paddlePos;
    }

    // Get position Ox
    private float GetPositionOx() {
        if ((sphere != null) & (gameStatus != null)) {
            return gameStatus.CheckAutoplayEnabled() ? sphere.transform.position.x : CalculateMousePositionX();
        } else {
            return CalculateMousePositionX();
        }
    }

    // Calculate mouse position
    private float CalculateMousePositionX() {
        return Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
}
