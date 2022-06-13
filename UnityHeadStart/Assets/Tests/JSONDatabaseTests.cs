using Assets.HeadStart.Features.Database.JSON;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

public class JSONDatabaseTests
{
    [Test]
    public void ReinitializePlayerJson()
    {
        // ARRANGE
        IJSONDatabase jsonDatabase = Substitute.For<IJSONDatabase>();

        // ACT
        jsonDatabase.RecreateDatabase();
 
        // ASSERT
        // Assert.AreEqual(true, true);
    }

    [Test]
    public void GetPlayer()
    {
        // ARRANGE
        IJSONDatabase jsonDatabase = Substitute.For<IJSONDatabase>();

        // ACT
        var playerExtension = jsonDatabase.GetPlayer();
        Debug.Log("playerExtension: " + playerExtension);
 
        // ASSERT
        // Assert.AreEqual(true, true);
    }
}
