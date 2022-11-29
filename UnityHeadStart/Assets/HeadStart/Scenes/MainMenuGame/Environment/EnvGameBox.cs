using UnityEngine;

public class EnvGameBox : MonoBehaviour
{
    public Rigidbody2D BallRb;

    void Start()
    {
        var x = Random.Range(0.2f, 0.8f);
        var y = Random.Range(0.2f, 0.8f);
        var dir = new Vector2(x, y);
        BallRb.AddForce(dir * 5);
    }
}
