using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour {
    //
    // Options
    [SerializeField] bool breakableBlock = true;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // Storage
    private int currentHits;
    private bool blockInvincible = false;

    // On Start
    private void Start() {
        currentHits = maxHits;
    }

    // On Update
    private void Update() {

    }

    // On collision Enter 2D
    private void OnCollisionEnter2D(Collision2D collision) {
        if (breakableBlock) {
            currentHits--;

            if (currentHits <= 0) {
                Destroy(gameObject);
            } else {
                ShowNextBlockHitSprite();
            }

            Debug.Log($"Object: {gameObject.name}. currentHits: {currentHits}");
        } else {
            Debug.Log($"Object: {gameObject.name}. Unbreakable");
        }   
    }

    // Show next hit sprite for block
    private void ShowNextBlockHitSprite() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int spriteIndex = currentHits - 1;

        if (spriteIndex < hitSprites.Length) {
            spriteRenderer.sprite = hitSprites[spriteIndex];
        } 
    }

    // Wait for seconds
    private IEnumerator SetBlockInvincibleInSeconds(float duration) {
        blockInvincible = true;
        yield return new WaitForSeconds(duration);
        blockInvincible = false;
    }
}