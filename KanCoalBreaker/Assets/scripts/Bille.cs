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
    }

    void Update()
    {
        if(!isLaunched){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isLaunched = true;
                rb.velocity = -Vector3.forward * speed;
                Debug.Log("La bille a été lancée !");
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
        Debug.Log("Collision avec : " + collision.gameObject.name);
        if (collision.gameObject != plateau)
        {
            Debug.Log("La bille a touché un objet autre que le plateau.");
            // Calcul de la r�flexion bas�e sur la normale de la collision
            Vector3 reflectDirection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            reflectDirection = new Vector3(reflectDirection.x + Random.Range(-angleVariance, angleVariance),reflectDirection.y,reflectDirection.z + Random.Range(-angleVariance, angleVariance));
            rb.velocity = reflectDirection * speed;
        }
    }
}
