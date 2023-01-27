using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform levels;
    public int currentLevel;

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("open_level");
        if (currentLevel != -1)//load levels on scene
        {
            GetLevel();
        }
        else
        {
            Destroy(levels.gameObject);
        }
    }

    public int tempNewLevel = -1;
    public void GetLevel()//if game is not scene-level based
    {
        if (currentLevel < levels.childCount)
        {
            levels.GetChild(currentLevel).gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("11");
            tempNewLevel = Random.Range(1, levels.childCount);
            levels.GetChild(tempNewLevel).gameObject.SetActive(true);

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (currentLevel < levels.childCount - 1)
            {
                
                levels.GetChild(currentLevel).gameObject.SetActive(false);
                currentLevel++;
            }
            GetLevel();
        }
    }

}