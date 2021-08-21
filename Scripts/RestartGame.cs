using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{

    Button restartButton;

    private void OnEnable()
    {
        JumporLeap.GameOverEvent += ShowPanel;
    }
    private void OnDisable()
    {
        JumporLeap.GameOverEvent -= ShowPanel;
    }

    // Start is called before the first frame update
    void Start()
    {
        restartButton = transform.Find("RestartButton").GetComponent<Button>();
        restartButton.onClick.AddListener(() => { RandomInitBox._instance.GameOn(); restartButton.gameObject.SetActive(false); });

        restartButton.gameObject.SetActive(false);
    }

    void ShowPanel()
    {
        restartButton.gameObject.SetActive(true);
    }
}
