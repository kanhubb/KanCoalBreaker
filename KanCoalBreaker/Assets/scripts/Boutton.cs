using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Ce script est utilisé pour gérer les actions des boutons dans les menus du jeu.
public class Boutton : MonoBehaviour
{
    // Start est appelé avant la mise à jour de la première image.
    void Start()
    {
        // Initialement vide car il n'y a pas besoin d'initialisation spécifique pour les boutons.
    }

    // Update est appelé à chaque image/frame. Il est vide ici car les boutons ne nécessitent pas de mise à jour continue.
    void Update()
    {
        
    }

    // Charge la scène de jeu principal lorsque le bouton "Nouveau Jeu" est cliqué.
    public void BtnNewGame()
    {
        SceneManager.LoadScene("GameScene"); // Utilise le SceneManager pour charger la scène de jeu spécifiée.
    }

    // Charge la scène des meilleurs scores lorsque le bouton "Meilleurs Scores" est cliqué.
    public void BtnBestScores()
    {
        SceneManager.LoadScene("BestScoreScene"); // Charge la scène qui affiche les meilleurs scores.
    }

    // Retourne au menu principal lorsque le bouton "Retour au Menu" est cliqué.
    public void BtnBackMenu()
    {
        SceneManager.LoadScene("KanMenu"); // Charge la scène correspondant au menu principal.
    }

    // Ferme l'application lorsque le bouton "Quitter" est cliqué.
    public void BtnQuit()
    {
        Application.Quit(); // Appel à la fonction Quit pour fermer l'application.
    }
}
