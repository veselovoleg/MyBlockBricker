using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStatus : MonoBehaviour {
    // Config
    [SerializeField] bool autoPlayEnabled = false;
    [SerializeField] string[] breackableBlocksTags;

    // State
    private int breackableBlocksCount = 0;

    // On Start
    private void Start() {
        breackableBlocksCount = CountAllBriackableObjects(breackableBlocksTags);
        Debug.Log($"breackableBlocksCount: {breackableBlocksCount}");
    }

    // On Update
    private void Update() {

    }

    /**
     * Count all breakable objects 
     * @param (string[]) tagsArray
     * @return (int) count
     **/
    private int CountAllBriackableObjects(string[] tagsArray) {
        return FindObjectsOfType<Block>()
            .Where(blockObject => tagsArray.Contains(blockObject.tag))
            .ToArray()
            .Length;
    }

    // breackableBlocksCount
    public void ReduceBlocksCount() {
        breackableBlocksCount--;

        if (breackableBlocksCount == 0) {
            Debug.Log("You Won!");
        }

        Debug.Log($"breackableBlocksCount: {breackableBlocksCount}");
    }

    // Check if autoplay enabled
    public bool CheckAutoplayEnabled() {
        return autoPlayEnabled;
    }
}
