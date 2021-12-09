// author - Samuel Adetunji
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 3f;

    public GameObject completeLevelUI;

    public GameObject parentEnemy;
    
    public Text banner;

    public bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        if(parentEnemy == null)
        {
            parentEnemy = GameObject.Find("Enemies");
        }

        if (parentEnemy.transform.childCount == 0)
        {
            Debug.Log("Game Over");
            banner.text = "Level Complete!\nMushrooms Exterminated";
            CompleteLevel();
        } else
        {
            banner.text = "Enemies Left - " + parentEnemy.transform.childCount.ToString();
        }
    }
    
    public void CompleteLevel()
    {
        Debug.Log("You Win!!");
        completeLevelUI.SetActive(true);
        EndGame();
    }

    public void EndGame () {
        if(gameOver == false)
        {
            gameOver = true;
            Debug.Log("GAME OVER, RESTART");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOver = false;
    }
}
