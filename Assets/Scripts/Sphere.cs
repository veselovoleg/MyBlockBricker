using UnityEngine;

public class Sphere : MonoBehaviour {
    // Options
    [SerializeField] Paddle paddle;
    // Random factor for bouncing
    [SerializeField] float randomFactorX = 0.25f;
    [SerializeField] float randomFactorY = 0.25f;
    [SerializeField] float MinimumSpeed = 25;
    [SerializeField] float MaximumSpeed = 30;
    //To prevent the ball from keep bouncing horizontally we enforce a minimum vertical movement
    [SerializeField] float MinimumVerticalMovement = 0.1f;


    // State
    GameStatus gameStatus;

    private bool ballLaunched = false;
    private Rigidbody2D ballRigidbody2D;
    private Vector2 paddleToBallVector;

    // On Start
    private void Start() {
        gameStatus = FindObjectOfType<GameStatus>();
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        paddleToBallVector = transform.position - paddle.transform.position;
    }

    // On Update
    private void Update() {
        if (!ballLaunched) {
            LockBallToPaddle();

            if (Input.GetMouseButtonDown(0) || ((gameStatus != null) & gameStatus.CheckAutoplayEnabled())) {
                LaunchBall();
            }
        }
    }

    // On Fixed Update
    private void FixedUpdate() {
        LimitBallSpeed();
    }

    // On collision Enter 2D
    private void OnCollisionEnter2D(Collision2D collision) {
        AddVelocityTweak();
    }
    // Add velocity tweak
    private void AddVelocityTweak() {
        Vector2 velocityTweak = new Vector2(Random.Range(1f, randomFactorX), Random.Range(0f, randomFactorY));

        if (ballRigidbody2D != null) {
            ballRigidbody2D.velocity += velocityTweak;
        }
    }

    // Limit Ball Speed
    private void LimitBallSpeed() {
        //Get current speed and direction
        Vector2 direction = ballRigidbody2D.velocity;
        float speed = direction.magnitude;
        direction.Normalize();

        if (speed < MinimumSpeed || speed > MaximumSpeed) {
            //Limit the speed so it always above min en below max
            speed = Mathf.Clamp(speed, MinimumSpeed, MaximumSpeed);

            //Apply the limit
            //Note that we don't use * Time.deltaTime here since we set the velocity once, not every frame.
            ballRigidbody2D.velocity = direction * speed;
        }
    }

    // Lock ball to paddle
    private void LockBallToPaddle() {
        Vector2 paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    // Launch ball
    public void LaunchBall() {
        //Create a random vector but make sure it always point "up" (z axis in this case) else it could be launched straight down
        Vector2 randomDirection = new Vector2(Random.Range(-1.0F, 1.0F), Mathf.Abs(Random.value));

        //Make sure we start at the minimum speed limit
        randomDirection = randomDirection.normalized * MinimumSpeed;

        //Apply it to the rigidbody so it keeps moving into that direction, untill it hits a block or wall
        ballRigidbody2D.velocity = randomDirection;

        ballLaunched = true;
    }

    // Get ballLaunched value
    public bool getBallLaunchedValue() {
        return ballLaunched;
    }
}
