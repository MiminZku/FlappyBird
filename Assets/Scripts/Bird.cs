using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Bird : MonoBehaviour
{
    public float flyPower;

    Rigidbody2D birdRb;
    // Start is called before the first frame update
    void Start()
    {
        birdRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, birdRb.velocity.y*5f);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            GetComponent<Animator>().SetTrigger("Fly");
            birdRb.AddForce(transform.up * flyPower, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            birdRb.AddForce(-transform.up * Time.deltaTime);
        }
    }

    private void Fly()
    {
    }
    private void Down()
    {
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Die");
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
