using UnityEngine;

public class Sphere : MonoBehaviour {
    // Options
    [SerializeField] Paddle paddle;
    [SerializeField] float launchBallX = 2f;
    [SerializeField] float launchBallY = 15f;
    [SerializeField] float randomFactor = 0.25f;
    [SerializeField] float maxHorizontalSpeed = 8;
    [SerializeField] float maxVerticalSpeed = 8;
    // Каким то образом вставить постоянную нетеряемую скорость
    [SerializeField] float sphereSpeed = 8;


    // State
    public bool ballLaunched = false;

    private Rigidbody2D ballRigidbody2D;
    private Vector2 paddleToBallVector;

    // On Start
    private void Start() {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        paddleToBallVector = transform.position - paddle.transform.position;
    }

    // On Update
    private void Update() {
        if (!ballLaunched) {
            LockBallToPaddle();

            if (Input.GetMouseButtonDown(0)) {
                LaunchBall();
            }
        }
    }

    // On Fixed Update
    private void FixedUpdate() {
        LimitBallSpeed();
    }

    // Limit Ball Speed
    private void LimitBallSpeed() {
        Vector2 velocity = ballRigidbody2D.velocity;

        // Horizontal
        if (velocity.x > maxHorizontalSpeed) {
            velocity.x = maxHorizontalSpeed * 0.5f;
        }

        // Vertical
        if (velocity.y > maxVerticalSpeed) {
            velocity.y = maxVerticalSpeed * 0.5f;
        }

        //if (velocity.y < -maxVerticalSpeed) velocity.y = -maxVerticalSpeed;
        //if (velocity.x < -maxHorizontalSpeed) velocity.x = -maxHorizontalSpeed;

        ballRigidbody2D.velocity = velocity;
    }

    // On collision Enter 2D
    private void OnCollisionEnter2D(Collision2D collision) {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

        if (ballRigidbody2D != null) {
            ballRigidbody2D.velocity += velocityTweak;
        }
    }

    // Lock ball to paddle
    private void LockBallToPaddle() {
        Vector2 paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    // Launch ball
    public void LaunchBall() {
        ballLaunched = true;
        ballRigidbody2D.velocity = new Vector2(launchBallX, launchBallY);
    }
}
