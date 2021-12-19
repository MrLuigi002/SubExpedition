using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private GameObject[] levelSegment;

    [SerializeField] private Transform segmentParent;

    [SerializeField] private float spawnOffset = 19.5f;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject controlsContainer;
    [SerializeField] private GameObject tutorialContainer;

    private PlayerController playerController;

    public int score;
    public int highScore;

    [SerializeField] private bool mainMenu = false;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        highScore = PlayerPrefs.GetInt("HighScore");

        if(PlayerPrefs.GetInt("HideTutorial") == 0 && !mainMenu)
        {
            tutorialContainer.SetActive(true);
        }

        if (mainMenu)
        {
            PlayerPrefs.SetInt("HideTutorial", 0);
        }
    }

    void Update()
    {
        score = playerController.currentMaxScore;

        if(score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);
        }

        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
    }

    public void ExpandLevel()
    {
        Vector3 spawnPosition = new Vector3(0, 0, Mathf.Round(player.transform.position.z) + spawnOffset);

        //Instantiate(levelSegment[Random.Range(0, levelSegment.Length)], spawnPosition, Quaternion.identity, segmentParent);

        GameObject segment = ObjectPool.SharedInstance.GetPooledSegment();

        if(segment != null)
        {
            segment.transform.position = spawnPosition;
            segment.transform.rotation = Quaternion.identity;
            segment.SetActive(true);
        }

        playerController.movementLimitZ = playerController.movementLimitZ + 15;
    }

    public void GameOver()
    {
        controlsContainer.SetActive(false);

        gameOverScreen.SetActive(true);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void HideTutorial()
    {
        tutorialContainer.SetActive(false);

        PlayerPrefs.SetInt("HideTutorial", 1);
    }
}
