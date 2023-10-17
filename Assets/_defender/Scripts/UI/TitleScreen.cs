using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Transform _logo;
    [SerializeField] GameObject _pressStartToPlay;
    
    private IUserInput _userInput;
    bool _startPressed;

    void Awake()
    {
        _userInput = FindObjectOfType<UserInput>();
    }
    private void OnEnable()
    {
        _pressStartToPlay.SetActive(false);
        _canvasGroup.alpha = 0f;
        _canvasGroup.DOFade(1f,3f);
        _logo.localScale = Vector3.zero;
        _userInput.OnStartPressed += HandleOnStart;
        _logo.DOScale(Vector3.one, 3f)
            .OnComplete(EnablePessStartToPlay);
    }

    private void OnDisable()
    {
        _userInput.OnStartPressed -= HandleOnStart;
    }

    private void Update()
    {
        if (_pressStartToPlay.activeSelf && _startPressed)
        {
            SceneManager.LoadScene("Main");
        }
    }

    private void HandleOnStart()
    {
        _startPressed = true;
    }

    void EnablePessStartToPlay()
    {
        _startPressed = false;
        _pressStartToPlay.SetActive(true);
    }
}
