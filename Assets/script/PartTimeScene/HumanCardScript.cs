using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class HumanCardScript : MonoBehaviour
{
    public GameObject humanCard;
    public Sprite[] sprites;

    public void Start()
    {
        humanCard.SetActive(false);
        SetRandomObject();
    }

    // ���� ������Ʈ �̹��� set
    public void SetRandomObject()
    {
        var random = Random.Range(0, sprites.Length - 1);
        humanCard.GetComponent<SpriteRenderer>().sprite = sprites[random];
    }
}
