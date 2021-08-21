using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumporLeap : MonoBehaviour
{
    public delegate void GameOver();
    public static GameOver GameOverEvent;

    public static JumporLeap _instance;

    private void Awake()
    {
        _instance = this;
    }

    int direction = 1;
    public void SetDirction(int dir)
    {
        direction = dir;
    }
    bool isPrepareToJump = false;
    float timer = 0;

    Sequence sq;
    public int distenceScale = 3;
    void Update()
    {
        if (transform.position.y - 0 < 0.1f)
        {
            GameOverEvent();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            isPrepareToJump = true;
            timer = 0;
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            isPrepareToJump = false;
            Vector3 endPos = new Vector3(transform.position.x + timer * distenceScale * direction, transform.position.y, transform.position.z + timer * distenceScale * (direction ^ 1));

            if (direction > 0)
            {
                if (Mathf.Abs(endPos.x - RandomInitBox._instance.GetCurrentTrans().position.x) < 0.2f)
                {
                    endPos = new Vector3(RandomInitBox._instance.GetCurrentTrans().position.x, transform.position.y, RandomInitBox._instance.GetCurrentTrans().position.z);
                }
            }
            else
            {
                if (Mathf.Abs(endPos.z - RandomInitBox._instance.GetCurrentTrans().position.z) < 0.2f)
                {
                    endPos = new Vector3(RandomInitBox._instance.GetCurrentTrans().position.x, transform.position.y, RandomInitBox._instance.GetCurrentTrans().position.z);
                }
            }

            sq = transform.DOJump(endPos, 1, 1, 1);
            transform.DORotate(new Vector3(0, 0, 360), 1,RotateMode.FastBeyond360);
            
        }
        if (sq != null && sq.position > 0.9f)
        {
            RandomInitBox._instance.RandInitBox();
            sq = null;
        }
        if (isPrepareToJump)
        {
            timer += Time.deltaTime;
        }
    }

}
