using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class PartTimeScript : MonoBehaviour
{
    public Sprite[] sprites;
    float[] time = { 7.0f, 10.0f, 15.0f, 11.3f, 12.3f, 19.3f };
    int cnt = 0;

    public int heart = 2;

    GameObject say;
    String[] sayText = { "�����", "������ּ���!", "�����~", "����", "�� ������?", "..." };

    public void Start()
    {
        say = GameObject.Find("SayText");
        gameObject.SetActive(false);
        SetTimeAfter();
    }

    public void SetTimeAfter()
    {
        Invoke("SetActiveObject", time[cnt]);
        Debug.Log(cnt);
        cnt++;
        Debug.Log(cnt);
    }

    void SetActiveObject()
    {
        gameObject.SetActive(true);
        SetRandomObject(); 
        say.GetComponent<SayTextScript>().RandomSaid(sayText);
    }

    // ���� ������Ʈ �̹��� set
    public void SetRandomObject()
    {
        var random = Random.Range(0, sprites.Length - 1);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[random];
    }
}
