using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Text score;
    [SerializeField] AudioSource over;
    bool gameOverHandled = false;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptHealth.score_live <= 0 && !gameOverHandled)
        {
            over.Play();
            StartCoroutine(GameOverRoutine());
            gameOverHandled = true;
        }
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        score.text = "GameOver";

        yield return new WaitForSeconds(3f); // Wait for additional 3 seconds

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        // Reset health to full (Assuming scriptHealth handles this)
        scriptHealth.score_live = 5;
    }
}
