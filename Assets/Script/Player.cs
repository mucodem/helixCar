using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    #region Singleton
    public static Player instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion  
    [SerializeField] Camera cam;
    [SerializeField] PathCreator path;
    public Text score,speedTx,maxPointTx;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject canvas;
    int point = 0;

    bool hit;
    bool speedster = false;
    MeshRenderer mesh;
    int tempSpeed;

    public int level;
    public int speed;
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        mesh = transform.GetChild(4).GetComponent<MeshRenderer>();
        if(PlayerPrefs.GetInt("level") >= 1)
        {
            CSVReader.instance.CsvFileRead(PlayerPrefs.GetInt("level"));
            //speedTx.text = PlayerPrefs.GetInt("speed").ToString();
           // maxPointTx.text = PlayerPrefs.GetInt("obsNum").ToString();
        }
        else
        {
            level = 1;
            CSVReader.instance.CsvFileRead(level);
            speedTx.text = speed.ToString();
            maxPointTx.text = PathCreator.instance.obsNum.ToString();
        }
       
       
    }


    void Update()
    {       
       transform.Translate(Time.deltaTime * speed * transform.forward);    
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Obstacles" && !speedster)
        {
            speed = 0;
            panel.SetActive(true);
        }
        else if (collision.transform.tag == "Point")
        {
            point++;
            score.text = point.ToString();
        }
        else if(collision.transform.tag == "SpeedUp")
        {
            speedster = true;
            collision.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(Speedster(20));
        }
        else if (collision.transform.tag == "SlowDown")
        {
            speedster = true;
            collision.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(Speedster(-20));
        }
        else if (collision.transform.tag == "End")
        {
            if (speedster) speed += tempSpeed;
            level += 1;
           
#if UNITY_ANDROID
            Handheld.Vibrate();
#endif
            PlayerPrefs.SetInt("level" , level);
            Debug.Log(PlayerPrefs.GetInt("level"));
            PlayerPrefs.SetInt("speed", speed);
            PlayerPrefs.SetInt("distance", PathCreator.instance.distance);
            PlayerPrefs.SetInt("obsNum", PathCreator.instance.obsNum);
            Continue();
        }
        else if(collision.transform.tag == "Coin")
        {
            StartCoroutine(CoinCreator());
        }
    }

    IEnumerator CoinCreator()
    {
        Vector2 pos = cam.WorldToScreenPoint(transform.position);
        for (int i = 0; i <= 5; i++)
        {
            Instantiate(coin, pos, Quaternion.identity, canvas.transform);
            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator Speedster(int speedChange)
    {
        tempSpeed = speedChange;
        speed += speedChange;
        speedTx.text = speed.ToString();
        mesh.enabled = false;

        yield return new WaitForSeconds(2);

        mesh.enabled = true;

        yield return new WaitForEndOfFrame();

        speed -= speedChange;
        speedster = false;
        speedTx.text = speed.ToString();
    }

    public void Restart()
    {     
        PlayerPrefs.DeleteAll();
        Continue();
    }

    public void Continue()
    {
        SceneManager.LoadScene(0);
        Debug.Log("deneme");
    }
}
