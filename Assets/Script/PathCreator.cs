using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathCreator : MonoBehaviour
{
    #region Singleton
    public static PathCreator instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion  
    [SerializeField] Player player;
    public GameObject[] obstacles;
    [SerializeField] Transform parentObj;
   
    GameObject cloneObs; // reference
    [SerializeField]GameObject endObj; // ned object prefab
    bool enough = true;
    float posEnd;

    public int distance;
    public int obsNum;


    void Start()
    {
        if(PlayerPrefs.GetInt("level") != 0)
        {
            if (PlayerPrefs.GetInt("level") % 5 == 0)
            {
                BonusPath();
            }
            else
                CreateOfPath();
        }
        else
            CreateOfPath();


    }

    public void CreateOfPath()
    {
        for (int i = 0; i <= obsNum; i++)
        {
            Randomizer();
            float randomValue = UnityEngine.Random.Range(0, 180);
            Quaternion myRot = Quaternion.Euler(0, 0, randomValue);
            float pos = 50 + distance * i;
            posEnd = pos;
            GameObject clone = Instantiate(cloneObs, new Vector3(0, 0, pos), myRot);
            clone.transform.parent = parentObj;
        }

        GameObject clone2 = Instantiate(endObj, new Vector3(0, 0, posEnd + 100), transform.rotation);
    }

    void BonusPath()
    {
        for (int i = 0; i <= obsNum; i++)
        {
            cloneObs = obstacles[3];
            float randomValue = UnityEngine.Random.Range(0, 180);
            Quaternion myRot = Quaternion.Euler(0, 0, randomValue);
            float pos = 50 + distance * i;
            posEnd = pos;
            GameObject clone = Instantiate(cloneObs, new Vector3(0, 0, pos), myRot);
            clone.transform.parent = parentObj;
        }

        GameObject clone2 = Instantiate(endObj, new Vector3(0, 0, posEnd + 100), transform.rotation);
    }

    void Randomizer()
    {             
        int rand = UnityEngine.Random.Range(0, 10);

        switch (rand)
        {
            case 0:
                cloneObs = obstacles[0];
                break;
            case 1:
                cloneObs = obstacles[0];
                break;
            case 2:
                cloneObs = obstacles[0];
                break;
            case 3:
                cloneObs = obstacles[0];
                break;
            case 4:
                cloneObs = obstacles[0];
                break;
            case 5:
                cloneObs = obstacles[0];
                break;
            case 6:
                cloneObs = obstacles[1];
                break;
            case 7:
                cloneObs = obstacles[1];
                break;
            case 8:
                cloneObs = obstacles[1];
                break;
            case 9:
                cloneObs = obstacles[2];
                break;
        }

        if (!enough && cloneObs == obstacles[2])
        {
            Randomizer();
        }
        else if (enough && cloneObs == obstacles[2])
        {
            enough = false;
        }            
    }
}
