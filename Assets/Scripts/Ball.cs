using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Configuration parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float launchXVector = 2f;
    [SerializeField] float launchYVector = 20f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //state
    Vector2 paddleToBallVector;

    //cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;


    bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            LockBallToPaddle();
        }
        LaunchOnMouseClick();
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePossition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePossition + paddleToBallVector;
    }

    private void LaunchOnMouseClick ()
    {
        if (Input.GetMouseButton(0))
        {
            myRigidBody2D.velocity = new Vector2(launchXVector, launchYVector);
            hasStarted = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0, randomFactor));
        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }

}
