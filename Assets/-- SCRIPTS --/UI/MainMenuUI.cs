using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons = new();
    private int _currentSelectedButton = 0;
    
    private void Start()
    {
        Debug.LogError("start");
        SelectButton(0);
    }

    private void Update()
    {
        if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.PAD_DOWN))
        {
            Debug.LogError("down");
            SelectButton(Math.Clamp(_currentSelectedButton + 1, 0, _buttons.Count - 1));
        } else if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.PAD_UP))
        {
            Debug.LogError("up");
            SelectButton(Math.Clamp(_currentSelectedButton - 1, 0, _buttons.Count - 1));
        } else if (CustomMidi.GetKeyDown(CustomMidi.MidiKey.NOTE_KEY))
        {
            Debug.LogError("validate");
            _buttons[_currentSelectedButton].onClick?.Invoke();
        }
    }

    private void SelectButton(int index)
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            Button b = _buttons[i];
            if (i == index)
            {
                b.transform.DOScale(1.5f, 0.1f);
                b.Select();
            }
            else
            {
                b.transform.DOScale(1f, 0.1f);
            }
        }

        _currentSelectedButton = index;
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
