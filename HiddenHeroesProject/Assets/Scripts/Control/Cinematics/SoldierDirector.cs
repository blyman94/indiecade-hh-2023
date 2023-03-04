using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDirector : MonoBehaviour
{
    public float[] YDirections;
    public float speedUpMultiplier;

    #region MonoBehaviour Methods
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soldier"))
        {
            int directionIndex = Random.Range(0, YDirections.Length);
            float ydir = YDirections[directionIndex];
            Soldier soldier = other.GetComponent<Soldier>();
            soldier.SendInRandomDir(ydir);
        }
    }
    #endregion

    private void SendInRandomDir(Rigidbody2D rb)
    {
        int directionIndex = Random.Range(0, YDirections.Length);
        float speed = rb.velocity.magnitude;
        rb.velocity = new Vector2(1.0f,YDirections[directionIndex]) * speed;
    }
}
