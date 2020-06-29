using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private int high, score;
    public List<Image> lives = new List<Image>(3);
    [SerializeField] private Text txtScore, txtHigh, txtLevel;
    private void Start () 
    {
        txtScore = GetComponentsInChildren<Text>()[1];
        txtHigh = GetComponentsInChildren<Text>()[0];
        txtLevel = GetComponentsInChildren<Text>()[2];

        for (int i = 0; i < 3 - GameManager.lives; i++)
        {
            Destroy(lives[lives.Count - 1]);
            lives.RemoveAt(lives.Count - 1);
        }
    }
	
    private void Update () 
    {
        score = GameManager.score;
        txtScore.text = "Score\n" + score;
        txtHigh.text = "High Score\n" + high;
        txtLevel.text = "Level\n" + (GameManager.Level + 1);
    }
}
