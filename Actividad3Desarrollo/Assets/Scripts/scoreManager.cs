using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    [SerializeField]
    public int actualScore;
    [SerializeField]
    private Text textScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "SCORE: " + actualScore;
    }

    public void addPoints(int pointsToAdd)
    {
        actualScore += pointsToAdd;
    }

    public void restarGame()
    {
        SceneManager.LoadScene(0);
    }
}
