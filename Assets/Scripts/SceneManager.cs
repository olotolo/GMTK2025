using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    [SerializeField] private string[] _sceneNames;
    [SerializeField] private Scene[] _scene;
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName); 
        ChangeLevelText();
    }

    int _currentLevel = -1;

    private void Start() {
        _currentLevel = 0;
        ChangeScene(_sceneNames[_currentLevel]);
    }
    
    public void LoadNextLevel() {
        _currentLevel++;
        ChangeScene(_sceneNames[_currentLevel]);
    }

    public void ReloadCurrentScene() {
        ChangeScene(_sceneNames[_currentLevel]);
    }


    [SerializeField] TextMeshProUGUI _levelText;
    public void ChangeLevelText() {
        _levelText.text = "Level: " + (_currentLevel + 1);
    }




}