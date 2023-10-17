using System;
using DG.Tweening;
using UnityEngine;


public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _scrollSpeed = 0.5f;
    private IUserInput _userInput;
    private GameManager _gameManager;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private PlayerShip PlayerShip => _gameManager.PlayerShip;

    void Awake()
    {
        _userInput = FindObjectOfType<UserInput>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void LateUpdate()
    {
        if (!PlayerShip || !_userInput.IsThrusting) return;
        var currentTextureOffset = _renderer.material.GetTextureOffset(MainTex);
        var distanceToScroll = Time.deltaTime * PlayerShip.Speed * PlayerShip.Direction * _scrollSpeed;
        var newOffset = currentTextureOffset + Vector2.right * distanceToScroll;
        _renderer.material.SetTextureOffset(MainTex, newOffset);
    }
}
