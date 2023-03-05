using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    [SerializeField]
    float slowdownAmount = 1f;
    public float scrollSpeed = 3.0f;

    public GameEvent startShakeEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.left * scrollSpeed) * Time.deltaTime;
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
            startShakeEvent.Raise();
        }
    }
   
}
