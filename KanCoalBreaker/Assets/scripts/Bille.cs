using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject plateau; // Assigner ce champ dans l'éditeur Unity
    public float angleVariance = 30f; // Définir la variance de l'angle en degrés
    public float constantSpeed = 10f; // Vitesse constante de la bille

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Assurer que la gravité n'affecte pas la bille
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.forward * constantSpeed;
            Debug.Log("La bille a été lancée !");
        }
    }

    void FixedUpdate()
    {
            rb.velocity = rb.velocity.normalized * constantSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision avec : " + collision.gameObject.name);
        if (collision.gameObject != plateau)
        {
            Debug.Log("La bille a touché un objet autre que le plateau.");
            // Ajout d'une variation aléatoire à l'angle de rebond
            float angle = Random.Range(-angleVariance, angleVariance);
            Vector3 newForceDirection = new Vector3(angle, 0, angle) * constantSpeed;
            rb.velocity = newForceDirection;
        }
    }
}
