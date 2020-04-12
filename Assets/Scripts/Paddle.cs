using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    // Options
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float screenWidthInUnits;

    // On Update 
    private void Update() {
        CalculatePaddlePosition();
    }

    // Calcualte paddle position on Ox
    private void CalculatePaddlePosition() {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(CalculateMousePositionX(), minX, maxX);
       // paddlePos.x = CalculateMousePositionX();
        transform.position = paddlePos;
    }

    // Calculate mouse position
    private float CalculateMousePositionX() {
        return Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
}
