using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomInitBox : MonoBehaviour
{


    public static RandomInitBox _instance;
    private void Awake()
    {
        _instance = this;
    }

    Vector3 cameraPos;

    Transform planeTrans;

    List<GameObject> boxs = new List<GameObject>();

    GameObject prefabBox;
    GameObject playerPrefab;
    Transform currentTrans;
    public Transform GetCurrentTrans()
    {
        return currentTrans;
    }


    Vector3 originalPos;

    GameObject player;

    public void GameOn()
    {
        while (boxs.Count > 0) 
        {
            GameObject tempGO = boxs[0];
            boxs.Remove(boxs[0]);
            Destroy(tempGO);
        }
        boxs.Add(GameObject.Instantiate(prefabBox, Vector3.zero, Quaternion.identity));
        boxs.Add(GameObject.Instantiate(prefabBox, new Vector3(2, 0, 0), Quaternion.identity));
        currentTrans = boxs[boxs.Count-1].transform;

        if (player != null)
        {
            Destroy(player);
        }
        player = GameObject.Instantiate(playerPrefab, new Vector3(0, 3, 0), Quaternion.identity);
        planeTrans.position = originalPos;
        JumporLeap._instance.SetDirction(1);
        transform.position = cameraPos;
    }


    void Start()
    {
        cameraPos = transform.position;

        planeTrans = GameObject.Find("Plane").transform;
        originalPos = planeTrans.position;
        playerPrefab = Resources.Load<GameObject>("Player");
        prefabBox = Resources.Load<GameObject>("Cube");
        GameOn();
    }

    public void RandInitBox()
    {
        int direction = (int)Mathf.Round(Random.Range(0.0f, 1.0f));
        JumporLeap._instance.SetDirction(direction);

        boxs.Add(GameObject.Instantiate(prefabBox, new Vector3(currentTrans.position.x + (2 * direction), 0, currentTrans.position.z + (2 * (direction ^ 1))), Quaternion.identity));
        currentTrans = boxs[boxs.Count - 1].transform;

        planeTrans.position = new Vector3(currentTrans.position.x, planeTrans.position.y, currentTrans.position.z);

        float scaleSize = Random.Range(0.5f, 1.5f);
        currentTrans.localScale = new Vector3(scaleSize, currentTrans.localScale.y, scaleSize);

        currentTrans.DOMove(new Vector3(currentTrans.position.x, 3, currentTrans.position.z), 1f).From();

        transform.DOMove(new Vector3(transform.position.x + (2 * direction), transform.position.y, transform.position.z + (2 * (direction ^ 1))), 1f);
        transform.DOLookAt(new Vector3(currentTrans.position.x - (2.5f * direction), 0, currentTrans.position.z - (2.5f * (direction ^ 1))), 1f);

        if (boxs.Count > 5)
        {
            Destroy(boxs[0]);
            boxs.Remove(boxs[0]);
        }
    }



}
