// author - Samuel Adetunji
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RemainingEnemies : MonoBehaviour
{
    public GameObject parentEnemy;
    //public Transform[] targets;
    public Text banner;

    // Update is called once per frame
    void Update()
    {
        if(parentEnemy == null)
        {
            parentEnemy = GameObject.Find("Enemies");
        }

        banner.text = "Enemies Left - " + parentEnemy.transform.childCount.ToString();

        if (parentEnemy.transform.childCount == 0)
        {
            Debug.Log("Game Over");
        }
    }
}
