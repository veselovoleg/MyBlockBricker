using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStatus : MonoBehaviour {
    // Config
    [SerializeField] string[] breackableBlocksTags;

    // State
    private int breackableBlocksCount;

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
}
