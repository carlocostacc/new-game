using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerbullet : MonoBehaviour
{

    public float speed = 7.5f;
    public Rigidbody2D theRb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D other){
        Destroy(gameObject);
    }
}
