using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    [SerializeField]
    float slowdownAmount = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 speed = new Vector2(-BikeGameManager.managerSpeed * Time.deltaTime * 3.3f, 0);
        transform.Translate(speed, Space.Self);
        if (Mathf.Abs(transform.position.x) > Mathf.Abs(BikeGameManager.minX))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("obstacle triggered");
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<BikeDriving>();
            player.slowDown(slowdownAmount);

        }
    }
   
}