﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject initialMenuPanel;
    public GameObject nextLevelPanel;
    public GameObject resultPanel;
    public GameObject settingPanel;

    public GameSettings settings;


    public Text resultText;

    public LevelChanger _lc50;
    public LevelChanger _lc80;
    public LevelChanger _testLC;


    private LevelChanger _chosenLC;

    public static UIManager instance;


    public bool useMouse = true;
    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }



    public void ShowNextLevelButton()
    {
        if (_chosenLC)
        {
            if (_chosenLC.HaveFinished())
            {
                _chosenLC.Finish();
                nextLevelPanel.SetActive(false);
                SaveToFile.instance.SaveResultFile();
                initialMenuPanel.SetActive(true);
            }
            else
                nextLevelPanel.SetActive(true);     
        }
    }

    public float GetTimeError()
    {
        return settings.timeError;
    }

    public float GetErrorPercentage()
    {
        return settings.tapError;
    }

    public float GetIsochronyInnerSpace()
    {
        return settings.isochronyInnerSpace;
    }
    public float GetIsochronyOuterSpace()
    {
        return settings.isochronyOuterSpace;
    }

    public int GetIsochronyRepetitions()
    {
        return settings.isochronyRepetitions;
    }

    public float GetOmothetyDelta()
    {
        return settings.omothetyDelta;
    }

    public int GetOmothetyRounds()
    {
        return settings.omothetyRounds;
    }

    public float GetSlowMultiplier()
    {
        return settings.omothetySlowMultiplier;
    }


    public float GetFastMultiplier()
    {
        return settings.omothetyFastMultiplier;
    }


    public void Load50bpmGame()
    {
        _lc50.Reset();
        settingPanel.SetActive(true);
        //_lc50.InstantiateNextLevel();
        initialMenuPanel.SetActive(false);
        _chosenLC = _lc50;
    }


    public void LoadTestGame()
    {
        _testLC.Reset();
        settingPanel.SetActive(true);
        //_lc50.InstantiateNextLevel();
        initialMenuPanel.SetActive(false);
        _chosenLC = _testLC;
    }


    public void Load50bpmGameNoSettings()
    {
        SaveToFile.instance.Init();
        _lc50.Reset();
        _lc50.InstantiateNextLevel();
        initialMenuPanel.SetActive(false);
        _chosenLC = _lc50;
    }

    public void Load80bpmGame()
    {
        SaveToFile.instance.Init();
        _lc80.Reset();
        settingPanel.SetActive(true);
        //_lc80.InstantiateNextLevel();
        initialMenuPanel.SetActive(false);
        _chosenLC = _lc80;
    }

    public void StartGame()
    {
        //TODO: leggi tutte le stronzate
        //TODO: salvale su file
        settingPanel.SetActive(false);
        _chosenLC.InstantiateNextLevel();
    }


    public void NextLevel()
    {
        if (resultPanel.activeInHierarchy)
        {
            resultText.text = "";
            resultPanel.SetActive(false);
        }

        if (_chosenLC)
        {
            if (_chosenLC == _lc80 && _chosenLC.actualLevel == 1)
            {
                if (!_chosenLC.loadedLevel.GetComponent<Level>().resultObject.WasGood())
                {
                    _chosenLC.Finish();
                    Load50bpmGameNoSettings();
                    nextLevelPanel.SetActive(false);
                    return;

                }    
            }


            if (_chosenLC.HaveFinished())
            {
                _chosenLC.Finish();
                SaveToFile.instance.SaveResultFile();
                initialMenuPanel.SetActive(true);
            }
            else  
                _chosenLC.InstantiateNextLevel();
        }
        nextLevelPanel.SetActive(false);
    }


    public void ShowResult(string result)
    {
        resultText.text = result;
        resultPanel.SetActive(true);
    }



}