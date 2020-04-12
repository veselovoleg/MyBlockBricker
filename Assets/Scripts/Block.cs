using UnityEngine;

public class Block : MonoBehaviour {
    // Options
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    private int currentHits;

    // On Start
    private void Start() {
        currentHits = maxHits;
    }

    // On Update
    private void Update() {

    }

    // On collision Enter 2D
    private void OnCollisionEnter2D(Collision2D collision) {
        currentHits--;
        Debug.Log($"Object: {gameObject.name}. currentHits: {currentHits}");

        if (currentHits <= 0) {
            Destroy(gameObject);
        } else {
            ShowNextBlockHitSprite();
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
}