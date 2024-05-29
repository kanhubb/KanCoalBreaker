using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject plateau; // Assigner ce champ dans l'éditeur Unity

    public GameObject bottomWall; // Préfabriqué de la bille
    public float angleVariance = 0.5f; // Définir la variance de l'angle en degrés
    public float speed = 10f; // Vitesse constante de la bille

    private bool isLaunched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; 
        gameObject.tag = "Ball";  
    }

    void Update()
    {
        if(!isLaunched){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isLaunched = true;
                rb.velocity = -Vector3.forward * speed;
            }
        }
    }
    void FixedUpdate()
    {
        if (isLaunched){
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != plateau)
        {
            Vector3 reflectDirection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            reflectDirection = new Vector3(reflectDirection.x + Random.Range(-angleVariance, angleVariance),reflectDirection.y,reflectDirection.z + Random.Range(-angleVariance, angleVariance));
            rb.velocity = reflectDirection * speed;
        }
        if (collision.gameObject == bottomWall)
        {
            GameManager.Instance.Lose();
        }
    }
    public void ResetBall()
    {
        StopBall(); // Assure that the ball is stopped before resetting
    }
    public void StopBall()
    {
        rb.velocity = Vector3.zero;
    }
}
