using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Enemy Settings")]
    public GameObject EnemyPrefab;
    public float enemyDestroyTime = 10f;

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;

    [Header("Panels")]
    public GameObject startMenu;
    public GameObject pauseMenu;

    [Header("Score UI")]
    public Text scoreText;
    public Text highScoreText;

    [Header("Audio")]
    public AudioClip explosionSound;  
    public AudioClip fireSound;       
    private AudioSource audioSource;  

    private int score = 0;
    private int highScore = 0;

    private void Awake()
    {
        instance = this;

        // Load High Score
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        //  AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        UpdateScoreUI();
        InvokeRepeating("InstantiateEnemy", 1f, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(true);
        }
    }

    void InstantiateEnemy()
    {
        Vector3 enemyPos = new Vector3(Random.Range(-10.2f, 9.5f), 11.8f);
        GameObject enemy = Instantiate(EnemyPrefab, enemyPos, Quaternion.Euler(0f, 0f, 180f));
        Destroy(enemy, enemyDestroyTime);
    }

    public void StartGameBtn()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1f;
        ResetScore();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
    public void PlayerDied()
    {
        
        startMenu.SetActive(true);

        
        Time.timeScale = 0f;

        
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        Debug.Log("Player has died. Game paused.");
    }

    
    public void RestartGame()
    {
        
        Time.timeScale = 1f;

       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }
    public void PlayExplosionSound()
    {
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
    }

  
    public void PlayFireSound()
    {
        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
}
