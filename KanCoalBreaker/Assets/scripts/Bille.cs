using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject plateau; // Assigner ce champ dans l'éditeur Unity
    public float angleVariance = 0.5f; // Définir la variance de l'angle en degrés
    public float speed = 10f; // Vitesse constante de la bille

    private bool isLaunched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Assurer que la gravité n'affecte pas la bille
        gameObject.tag = "Ball";  // Assurer que le tag de la balle est correctement défini

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
    }
}
