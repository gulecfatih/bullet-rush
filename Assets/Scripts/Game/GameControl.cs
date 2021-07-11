using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    int enemyPoint=0;
    int SliderMax=0;
    public Text enemyText;
    public Text winLose;
    public Button resbutton;

    private void Start()
    {
        Time.timeScale = 1;
        EnemyAdd();
        enemyPoint = enemyList.Count;
        SliderMax = enemyPoint;
        enemyText.text = SliderMax.ToString();
        winLose.gameObject.SetActive(false);
        resbutton.gameObject.SetActive(false);
    }

    void EnemyAdd()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) // Düþman Sayýsýný Bulma
        {
            enemyList.Add(enemy);
        }


    }

    void EnemyCount() // kalan düþman bulma
    {
        enemyPoint = enemyList.Count;
    }
    int deadEnemy = 0;
    public void EnemyDead(GameObject enemy) // Listeden ölen düþmaný çýkarma
    {
        deadEnemy++;
        enemyList.Remove(enemy.gameObject);
        EnemyCount();
        FindObjectOfType<SliderControl>().SliderDeger(SliderMax, deadEnemy);
        enemyText.text = enemyPoint.ToString();
        if (enemyPoint == 0)
        {
            Time.timeScale = 0;
            winLose.text = "Kazandýn";
            winLose.gameObject.SetActive(true);
            resbutton.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }



}
