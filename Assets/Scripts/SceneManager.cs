using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {
    [SerializeField] private string[] _sceneNames;
    [SerializeField] private Scene[] _scene;
    [SerializeField] Image _blackScreen;

    [SerializeField] private float fadeSpeed = 0.01f;
    [SerializeField] private float fadeStep = 0.01f;

    public void RestartScene()
    {
        StartFadeToScene(_sceneNames[_currentLevel]);
    }
    public void ChangeScene(string sceneName) {
        StartFadeToScene(sceneName);
    }

    private bool _isFading = false;

    public void StartFadeToScene(string sceneName) {
        if (!_isFading)
            StartCoroutine(FadeAndSwitchScene(sceneName));
    }

    private IEnumerator FadeAndSwitchScene(string sceneName) {
        _isFading = true;

        // Fade to black
        while (_blackScreen.color.a < 1f) {
            Color color = _blackScreen.color;
            color.a += fadeStep;
            _blackScreen.color = color;
            yield return new WaitForSeconds(fadeSpeed);
        }

        // Load new scene
        SceneManager.LoadScene(sceneName);

        //Color _color = _blackScreen.color;
        //_color.a = 0;
        //_blackScreen.color = _color;
        _currentLevelUI.SetActive(true);
        Destroy(_pressButtonUI);
        ChangeLevelText();

        // Fade to white
        while (_blackScreen.color.a > 0f)
        {
            Color color = _blackScreen.color;
            color.a -= fadeStep;
            _blackScreen.color = color;
            yield return new WaitForSeconds(fadeSpeed);
        }

        _isFading = false;
    }

    public IEnumerator FadeToNormal() {
        Color color = _blackScreen.color;
        color.a = Mathf.Max(0, 0f);
        _blackScreen.color = color;

        yield return new WaitForSeconds(fadeSpeed);
    }





    public int _currentLevel = -1;
    [SerializeField] GameObject _currentLevelUI;
    [SerializeField] GameObject _pressButtonUI;

    private void Start() {
        _currentLevelUI.SetActive(false);
    }

    [SerializeField] GameObject _madeByUI;
    private void StartFirstLevel() {
        _currentLevel = 0;
        Destroy(_madeByUI);
        ChangeScene(_sceneNames[_currentLevel]);
    }

    bool _started = false;

    private void Update() {
        if (Input.anyKeyDown && !_started) {
            // Filter out mouse buttons
            if (!Input.GetMouseButtonDown(0) &&
                !Input.GetMouseButtonDown(1) &&
                !Input.GetMouseButtonDown(2)) {
                StartFirstLevel();
                _started = true;
            }
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