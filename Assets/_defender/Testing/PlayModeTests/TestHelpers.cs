using System.Collections;
using _defender.Scripts.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class TestHelpers
{
    public static IEnumerator LoadScene(string sceneName)
    {
        var loadSceneTask = SceneManager.LoadSceneAsync(sceneName);
        while (!loadSceneTask.isDone) yield return null;
    }

    public static PlayerShip GetPlayerShip()
    {
        return GameObject.FindObjectOfType<PlayerShip>();
    }

    public static GameManager GetGameManager()
    {
        return GameObject.FindObjectOfType<GameManager>();
    }

    public static Human GetHuman()
    {
        return GameObject.FindObjectOfType<Human>();
    }
}