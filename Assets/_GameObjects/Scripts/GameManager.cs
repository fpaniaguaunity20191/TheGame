using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int points=0;
    public int GetPoints()
    {
        return points;
    }
    private Text txtPoints;
    private void Awake()
    {
        SceneManager.LoadScene("CanvasScene", LoadSceneMode.Additive);
    }
    private void Start()
    {
        txtPoints = GameObject.Find("TxtPoints").GetComponent<Text>();
        txtPoints.text = points.ToString();
    }

    public void AddPoints(int _points)
    {
        this.points += _points;
        txtPoints.text = this.points.ToString();
    }
}
