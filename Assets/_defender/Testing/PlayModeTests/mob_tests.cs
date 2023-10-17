using System.Collections;
using _defender.Scripts.Attributes;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class mob_tests
{
    private GameManager _gameManager;
    private Human _human;
    
    [UnitySetUp]
    public IEnumerator SetupTest()
    {
        UserInput.Instance = Substitute.For<IUserInput>();
        yield return TestHelpers.LoadScene("MobTests");
        _gameManager = TestHelpers.GetGameManager();
        _gameManager.StartGame();
        yield return new WaitForEndOfFrame();
        _human = TestHelpers.GetHuman();
    }

    [UnityTest]
    public IEnumerator human_should_scroll_when_player_ship_thrusts()
    {
        Vector3 startPosition = _human.transform.position;
        UserInput.Instance.IsThrusting.Returns(true);
        yield return new WaitForSeconds(0.25f);
        Assert.IsTrue(_human.transform.position.x < startPosition.x, "Expected human position to be left of start position");
    }
}