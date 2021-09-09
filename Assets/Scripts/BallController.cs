using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] bool isLoaded;
    [SerializeField] GameObject ballPrefab;
    GameObject loadedBall;
    float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        LoadBall();   
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoaded)
        {
            Aim();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }
        else
        {
            // TODO should only load ball *after* ball is scored
            // need to keep track of time it takes to fire before reloading
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadBall();
            }
        }
    }

    // TODO addForce to ball, update isLoaded
    void Fire()
    {
        loadedBall.GetComponent<Rigidbody>().AddForce(Vector3.left * force);
        loadedBall.GetComponent<Rigidbody>().AddForce(Vector3.forward* horizontalInput);
        loadedBall.transform.SetParent(null, true);
        isLoaded = false;
        loadedBall = null;
    }


    void LoadBall()
    {
        loadedBall = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        loadedBall.transform.SetParent(this.transform);
        // Zero to parent position
        //loadedBall.transform.localPosition = Vector3.zero;
        loadedBall.transform.localPosition = new Vector3(0, 0, -2.5f);
        isLoaded = true;
    }

    // Move Paddle left and right
    void Aim()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        float speed = horizontalInput * Time.deltaTime * 5;
        transform.Translate(speed, 0, 0);
    }
}
