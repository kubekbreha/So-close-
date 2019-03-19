using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class Player : MonoBehaviour
{
    private readonly Vector3 VectorXY = new Vector3(1, 1, 0f);

    public string Id { get; set; }

    [SerializeField] private float speed;

    [SerializeField] private float rotateSpeed;

    [SerializeField] private float fireSpeed;

    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float bulletOffset = 1.33f;

    [SerializeField] private int bulletVelocity = 1500;

    public bool Dead { get; private set; }
    
    public bool Invulnerable { get; private set; }

    private Rigidbody2D rigidBody;

    private Animator animator;

    private Controller controller;

    private readonly Random random = new Random();

    private float lastFireTime;

    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        controller = GameController.Instance.GetController(Id);
        StartCoroutine(Invulnerability());
    }

    void Update()
    {
        if (!Dead)
        {
            var movement = controller.DirectionVector;
            var vector = Vector3.Scale(movement, VectorXY);
            //move
            rigidBody.AddForce(vector);

            //rotate to direction
            var angle = Vector2.SignedAngle(Vector2.up, vector);
            rigidBody.MoveRotation(angle);

            //set animation
            if (Math.Abs(rigidBody.velocity.x) > 0.05 || Math.Abs(rigidBody.velocity.y) > 0.05)
                animator.SetTrigger("Walk");
            else
                animator.SetTrigger("Stand");

            if (controller.Fire > 0)
            {
                if (Time.time >= lastFireTime + fireSpeed)
                {
                    lastFireTime = Time.time;
                    controller.Fire = 0;
                    StartCoroutine(Throw());
                }
            }
        }
    }

    private IEnumerator Throw()
    {
        if (Invulnerable) yield break;
        animator.SetTrigger("Throw");
        yield return new WaitForSeconds(0.4f);

        GameObject projectile = (GameObject) Instantiate(projectilePrefab,
            transform.position + transform.up*bulletOffset + transform.right*bulletOffset, transform.rotation);
        projectile.transform.SetParent(this.transform);
        projectile.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletVelocity);
    }

    public void GotHit() => StartCoroutine(Die());

    private IEnumerator Die()
    {
        
        if (Dead) yield break;
        Dead = true;
        animator.SetTrigger("Dead");
        rigidBody.AddTorque(500);
        yield return new WaitForSeconds(2);
        animator.SetTrigger("Stand");
        StartCoroutine(Invulnerability());
        SpawnPlayer();
        Dead = false;
    }

    private IEnumerator Invulnerability()
    {
        Invulnerable = true;
        var waitGivenTime = new WaitForSeconds(0.1f);
        for (var i = 0; i < 15; i++)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return waitGivenTime;
            GetComponent<SpriteRenderer>().enabled = true;
            yield return waitGivenTime;
        }
        Invulnerable = false;
    }

    public void SpawnPlayer()
    {
        var x = random.Next(-30, 30);
        var y = random.Next(-10, 10);
        transform.position = new Vector2(x, y);
    }   
}