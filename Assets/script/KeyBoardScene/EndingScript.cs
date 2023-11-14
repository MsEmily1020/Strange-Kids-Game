using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using TMPro;
using Random = UnityEngine.Random;

public class EndingScript : MonoBehaviour
{
    public GameObject panel;
    public int heart = 3;
    String text;

    void Start()
    {
        panel.SetActive(false);
    }

    public void SetActiveObject()
    {
        panel.SetActive(true);

        if(heart == 2)
        {
            panel.transform.Find("content").GetComponent<TMP_Text>().text = "�ٸ��� �� ���� ��� ����ϰڽ��ϴ�! \n �����ε� ������ �ٸ� ���� ��Ȱ ������~!";
        }

        else
        {
            panel.transform.Find("content").GetComponent<TMP_Text>().text = "�ٸ��� �� ����� �츮 ������ݾƿ�!�� \n ���ݺ��Ͷ� ������ Ÿ��ġ�� ���� ���� �����غ��ƿ�!";
        }
    }
}
