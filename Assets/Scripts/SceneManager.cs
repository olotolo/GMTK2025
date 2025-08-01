using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] GameObject _currentLevelUI;
    [SerializeField] GameObject _pressButtonUI;

    private void Start() {
        _currentLevelUI.SetActive(false);
    }

    private void StartFirstLevel() {
        _currentLevel = 0;
        ChangeScene(_sceneNames[_currentLevel]);
        _currentLevelUI.SetActive(true);
        Destroy(_pressButtonUI);
    }

    bool _started = false;

    private void Update() {
        if(Input.anyKeyDown && !_started) {
            StartFirstLevel();
            _started = true;
        }
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