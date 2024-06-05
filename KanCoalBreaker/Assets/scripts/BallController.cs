using UnityEngine;

// Contrôle le comportement de la bille dans le jeu, y compris le lancement et les collisions.
public class BallController : MonoBehaviour
{
    private Rigidbody rb;  // Composant Rigidbody de la bille pour gérer la physique.
    public GameObject plateau; // Plateau avec lequel la bille peut interagir sans rebondir.

    public GameObject bottomWall; // Objet qui, lorsqu'il est touché par la bille, déclenche une perte.
    public float angleVariance = 0.5f; // Variance de l'angle de rebond après une collision, pour ajouter de l'aléatoire.
    public float speed = 10f; // Vitesse constante de la bille.

    private bool isLaunched = false; // État de lancement de la bille, pour éviter des redémarrages intempestifs.

    // Initialisation du composant Rigidbody et configuration initiale de la bille.
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Récupère le composant Rigidbody attaché à cet objet.
        rb.useGravity = false; // Désactive la gravité pour la bille.
        gameObject.tag = "Ball"; // Assigne le tag "Ball" à l'objet pour identification facile dans d'autres scripts.
    }

    // Gère l'entrée de l'utilisateur pour lancer la bille.
    void Update()
    {
        if(!isLaunched){ // Vérifie si la bille n'est pas déjà lancée.
            if (Input.GetKeyDown(KeyCode.Space)) // Écoute la touche espace pour lancer la bille.
            {
                isLaunched = true; // Marque la bille comme lancée.
                rb.velocity = -Vector3.forward * speed; // Propulse la bille en avant à la vitesse définie.
            }
        }
    }

    // Assure que la bille maintienne sa vitesse après les mises à jour de physique.
    void FixedUpdate()
    {
        if (isLaunched){ // Vérifie si la bille a été lancée.
            rb.velocity = rb.velocity.normalized * speed; // Normalise la vitesse pour maintenir la vitesse constante.
        }
    }

    // Gère les collisions de la bille avec d'autres objets.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != plateau) // Si l'objet touché n'est pas le plateau.
        {
            // Calcule la nouvelle direction de la bille après la collision.
            Vector3 reflectDirection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            // Ajoute une variance aléatoire à la direction de réflexion pour simuler un rebond irrégulier.
            reflectDirection = new Vector3(reflectDirection.x + Random.Range(-angleVariance, angleVariance),reflectDirection.y,reflectDirection.z + Random.Range(-angleVariance, angleVariance));
            rb.velocity = reflectDirection * speed; // Applique la nouvelle vitesse avec la direction ajustée.
        }
        if (collision.gameObject == bottomWall) // Si l'objet touché est le mur du bas.
        {
            GameManager.Instance.Lose(); // Appelle la fonction Lose du GameManager pour gérer la perte.
        }
    }

    // Réinitialise la position de la bille et arrête son mouvement.
    public void ResetBall()
    {
        StopBall(); // Assure que la bille est arrêtée avant de réinitialiser.
    }

    // Arrête le mouvement de la bille en réglant sa vitesse à zéro.
    public void StopBall()
    {
        rb.velocity = Vector3.zero; // Arrête la bille en fixant sa vitesse à zéro.
    }
    public void IncreaseSpeed()
    {
        speed *= 1.4f;  // Augmente la vitesse de 20%
    }

    public void DecreaseSpeed()
    {
        speed *= 0.6f;  // Réduit la vitesse de 20%
    }

}
