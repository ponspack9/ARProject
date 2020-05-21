using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Spawner spawn;
    public Player player;

    public GameObject text_gameOver;
    public GameObject text_Start;
    public GameObject mainmenu;
    public static bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        text_gameOver.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void StartGame()
    {
        text_gameOver.SetActive(false);
        mainmenu.SetActive(false);
        spawn.StartGame();
        player.StartGame();
        text_Start.SetActive(false);
        gameOver = false;

    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            text_gameOver.SetActive(true);
            mainmenu.SetActive(true);

        }
    }
}
