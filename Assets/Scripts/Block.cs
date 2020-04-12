using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour {
    //
    // Options
    [SerializeField] bool breakableBlock = true;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // Storage
    GameStatus gameStatus;

    private int currentHits;
    private int hitSpriteIndex = 0;
    private bool blockInvincible = false;

    // On Start
    private void Start() {
        gameStatus = FindObjectOfType<GameStatus>();
        currentHits = maxHits;
    }

    // On Update
    private void Update() {

    }

    // On collision Enter 2D
    private void OnCollisionEnter2D(Collision2D collision) {
        if (breakableBlock) {
            CalculateBlockHits();
        } else {
            Debug.Log($"Object: {gameObject.name}. Unbreakable");
        }   
    }
    // Calculate block hits
    private void CalculateBlockHits() {
        currentHits--;

        if (currentHits <= 0) {
            Destroy(gameObject);

            if (gameStatus != null) {
                gameStatus.ReduceBlocksCount();
            }
        } else {
            ShowNextBlockHitSprite();
        }

        Debug.Log($"Object: {gameObject.name}. currentHits: {currentHits}");
    }

    // Show next hit sprite for block
    private void ShowNextBlockHitSprite() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (hitSprites.Length > 0) {
            if (hitSpriteIndex < hitSprites.Length) {
                spriteRenderer.sprite = hitSprites[hitSpriteIndex];
                hitSpriteIndex++;
            } else {
                spriteRenderer.sprite = hitSprites[0];
            }
        }
        

        /*
        int spriteIndex = currentHits - 1;

        if (spriteIndex < hitSprites.Length) {
            spriteRenderer.sprite = hitSprites[spriteIndex];
        } 
        */
    }

    // Wait for seconds
    private IEnumerator SetBlockInvincibleInSeconds(float duration) {
        blockInvincible = true;
        yield return new WaitForSeconds(duration);
        blockInvincible = false;
    }
}