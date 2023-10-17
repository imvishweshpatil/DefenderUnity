using System;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class player_ship_movement_tests
{
    private PlayerShip _playerShip;

    [UnitySetUp]
    public IEnumerator TestSetup()
    {
        UserInput.Instance = Substitute.For<IUserInput>();
        yield return TestHelpers.LoadScene("PlayerShipTests");
        _playerShip = TestHelpers.GetPlayerShip();
    }
    [UnityTest]
    public IEnumerator player_ship_flip_direction_test()
    {
        Assert.AreEqual(1, _playerShip.Direction);
        UserInput.Instance.OnFlipPressed += Raise.Event<Action>();
        yield return null;
        Assert.AreEqual(-1, _playerShip.Direction);
    }
    [UnityTest]
    public IEnumerator player_ship_move_up_test()
    {
        Assert.AreEqual(Vector3.zero, _playerShip.transform.position);
        UserInput.Instance.UpPressed.Returns(true);
        yield return new WaitForSeconds(0.25f);
        Assert.IsTrue(_playerShip.transform.position.y > 0);
    }
    [UnityTest]
    public IEnumerator player_ship_move_down_test()
    {
        Assert.AreEqual(Vector3.zero, _playerShip.transform.position);
        UserInput.Instance.DownPressed.Returns(true);
        yield return new WaitForSeconds(0.25f);
        Assert.IsTrue(_playerShip.transform.position.y < 0);
    }

    [UnityTest]
    public IEnumerator background_should_scroll_when_ship_thrust()
    {
        // Arrange
        BackgroundScroller bgScroller = GameObject.FindObjectOfType<BackgroundScroller>();
        Renderer renderer = bgScroller.GetComponent<Renderer>();
        Vector2 offset = renderer.material.GetTextureOffset("_MainTex");
        Assert.AreEqual(Vector2.zero, offset, "Material offset should initially be zero");
        
        // Act
        UserInput.Instance.IsThrusting.Returns(true);
        yield return new WaitForSeconds(0.25f);
        
        // Assert
        offset = renderer.material.GetTextureOffset("_MainTex");
        Assert.IsTrue(offset.x > 0, "offset should be greater than zero after ship thrusts");
    }
}