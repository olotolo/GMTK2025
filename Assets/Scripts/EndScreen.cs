using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject _restartText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(restart());
    }
    bool _restartActive = false;
    public float fadeDuration = 2f;
    public IEnumerator restart() {
        yield return new WaitForSeconds(5);

        _restartText.SetActive(true);
        TextMeshProUGUI text = _restartText.GetComponent<TextMeshProUGUI>();

        Color color = text.color;
        color.a = 0f;
        text.color = color;

        _restartActive = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration) {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            color.a = alpha;
            text.color = color;
            yield return null;
        }

        // Ensure it's fully visible at the end
        color.a = 1f;
        text.color = color;
    }

    private void Update() {
        if (_restartActive) {
            if (Input.anyKeyDown) {
                if (!Input.GetMouseButtonDown(0) &&
                    !Input.GetMouseButtonDown(1) &&
                    !Input.GetMouseButtonDown(2)) {
                    SceneManager.LoadScene("Level_01");
                }
            }
        }
    }


}
