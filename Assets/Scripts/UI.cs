using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{    
    public Text Score;
    public GameObject Back;
    public PlayerControl Player;
    private static int mScore = 0;

    // Press play button
    public void Play()
    {
        Back.gameObject.GetComponent<Animator>().Play("ScreenPlay");        
        Asteroids.Clear();
        Asteroids.isActiveCreating = true;
        Score.text = "0";
        mScore = 0;

        Player.Restart();
    }

    // This method is called when losing
    public void Loose()
    {
        Back.gameObject.GetComponent<Animator>().Play("ScreenLoose");
        Asteroids.isActiveCreating = false;
    }

    // This method is called when receiving scores
    public void AddScore()
    {
        mScore++;
        Score.text = "" + mScore;
    }
}
