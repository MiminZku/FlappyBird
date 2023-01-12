using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Bird : MonoBehaviour
{
    public float flyPower;
    public bool isDead = false;
    public ObstacleGenerator og;

    Rigidbody2D birdRb;
    // Start is called before the first frame update
    void Start()
    {
        birdRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, birdRb.velocity.y*3f);
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !og.isPaused)
        {
            Fly();
        }
        if (Input.GetKey(KeyCode.DownArrow) && !og.isPaused)
        {
            Down();
        }
    }

    private void Fly()
    {
        GetComponent<Animator>().SetTrigger("Fly");
        birdRb.AddForce(transform.up * flyPower, ForceMode2D.Impulse);
    }
    private void Down()
    {
        birdRb.AddForce(-transform.up * 500 * Time.deltaTime, ForceMode2D.Force);
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
        gameObject.SetActive(false);
        isDead = true;
    }
}
