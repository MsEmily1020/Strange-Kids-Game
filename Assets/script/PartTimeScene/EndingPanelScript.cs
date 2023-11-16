using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
using TMPro;

public class EndingPanelScript : MonoBehaviour
{
    public GameObject endingPanel;
    int heart;

    public void Start()
    {
        endingPanel.SetActive(false);    
    }

    public void Ending()
    {
        heart = GameObject.Find("Human").GetComponent<PartTimeScript>().heart;
        GameObject.Find("SaveData").GetComponent<SaveClearData>().clear2 = true;

        if(heart == 1)
        {
            endingPanel.transform.Find("content").GetComponent<TMP_Text>().text = "��.. ������ �����߾�! \n (( �ź��� Ȯ�ε� ���� �� 19�� �̸� ������ǰ�� �Ǹ��ߴٰ��� �������� �����ž�!��))";
        }

        else if(heart == 0)
        {
            GameObject.Find("SaveData").GetComponent<SaveClearData>().clear--;
            endingPanel.transform.Find("content").GetComponent<TMP_Text>().text = "�ź��� Ȯ�� ���� �� 19�� �̸� ������ǰ�� �Ǹ��� �������� �Ծ�Ф�";
        }

        endingPanel.SetActive(true);
    }
}
