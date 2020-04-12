using UnityEngine;

public class Sphere : MonoBehaviour {
    // Options
    [SerializeField] Paddle paddle;
    // Random factor for bouncing
    [SerializeField] float randomFactor = 0.25f;
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
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

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

        //Make sure the ball never goes straight horizotal else it could never come down to the paddle.
        if (direction.y > -MinimumVerticalMovement && direction.y < MinimumVerticalMovement) {
            //Adjust the y, make sure it keeps going into the direction it was going (up or down)
            direction.y = direction.y < 0 ? -MinimumVerticalMovement : MinimumVerticalMovement;

            //Adjust the x also as x + y = 1
            direction.x = direction.x < 0 ? -1 + MinimumVerticalMovement : 1 - MinimumVerticalMovement;

            //Apply it back to the ball
            ballRigidbody2D.velocity = direction * speed;
        }

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
