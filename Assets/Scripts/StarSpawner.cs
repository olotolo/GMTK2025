using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public int numberOfStars;
    public float spawnWidth;
    public float spawnHeight;
    public float doAnimate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < numberOfStars; i++)
        {
            float xpos = Random.Range(-spawnWidth, spawnWidth);
            float ypos = Random.Range(-spawnHeight, spawnHeight);
            Vector3 _loc = new Vector3(xpos, ypos, 10);
            //GameObject star = Instantiate(starPrefab, _loc, Quaternion.identity);
            GameObject star = Instantiate(starPrefab, this.transform);
            star.transform.localPosition = _loc;

            Animator _starAnimator = star.GetComponent<Animator>();
            float _startTime = Random.Range(0f, 1f);
            _starAnimator.Play(0, -1, _startTime);

            bool _doAnimate = Random.Range(0f, 1f) < doAnimate;
            if (_doAnimate)
                _starAnimator.speed = 0f;
            
        }
    }
}
