using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public bool gameIsOver = false;

    public bool godMode = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            this.enabled = false;
        }
    }

    private void GameOver()
    {
        print("Gameover");
        if (godMode)
        {
            return;
        }
        gameIsOver = true;
    }

    public void LastChance()
    {
        StartCoroutine(WaitForLastInput());
        StartCoroutine(GameOverIfNotInput());
    }

    private IEnumerator WaitForLastInput()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        StopAllCoroutines();
        print("Salvo pelo gongo");
    }

    private IEnumerator GameOverIfNotInput()
    {
        yield return new WaitForSeconds(0.1f);
        GameOver();
        StopAllCoroutines();
    }
}
