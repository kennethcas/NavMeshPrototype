using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public static GameManager instance;

    public Transform player;

    [SerializeField] //mostly used for private >//<
    public bool isGameOver = false;
    GameObject ui_GameOverPage;

    GameObject[] enemyObjects;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //MAKING AN ARRAY FOR ENEMYOBJECTS IN SCENE
        
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        print(enemyObjects.Length);
        //RESTART SCENE WITH R
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

        //
        if (enemyObjects.Length == 0)
        {
            Debug.Log("NO MORE ENEMIES IN SCENE");
        }

        
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        isGameOver = true;
        ui_GameOverPage.SetActive(true);
    }
}
