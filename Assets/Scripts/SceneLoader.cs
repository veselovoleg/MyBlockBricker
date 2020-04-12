using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    // State
    string currentSceneName;

    // On Start
    private void Start() {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    /**
     * Load scene by name
     * @param (string) sceneName
     **/
    public void LoadSceneByName(string sceneName = null) {
        SceneManager.LoadScene(sceneName != null ? sceneName : currentSceneName);
    }
}
