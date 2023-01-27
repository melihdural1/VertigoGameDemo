using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Rullet;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject menuCanvas, gameCanvas;

    public Button startButton, backButton;

    public SpinRullet spinControl;

    private void Awake()
    {
        EventController.UpdateStateEvent += UpdateUI;
    }

    private void OnValidate()
    {
        startButton.onClick.AddListener(() => StartGameClick());
        backButton.onClick.AddListener(()=> BackToMenu());
    }

    private void OnDestroy()
    {
        EventController.UpdateStateEvent -= UpdateUI;
    }

    private void Start()
    {
        EventController.instance.UpdateStateEventRun(GameState.menu);
    }
        

    public void UpdateUI(GameState state)
    {
        menuCanvas.SetActive(false);
        gameCanvas.SetActive(false);

        switch (state)
        {
            case GameState.menu:
                menuCanvas.SetActive(true);
                //TODO set current level text, coin text etc. in here
                    

                break;
            case GameState.game:
                gameCanvas.SetActive(true);
                //TODO if necessary set game canvas texts etc.
                EventController.OnGameStart?.Invoke();

                break;
        }
    }

    public void StartGameClick()
    {
        EventController.instance.UpdateStateEventRun(GameState.game);
    }

    public void BackToMenu()
    {
        EventController.instance.UpdateStateEventRun(GameState.menu);
    }
}