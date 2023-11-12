using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
using System.Text.RegularExpressions;

public class HumanCardCheckScript : MonoBehaviour
{
    GameObject product;
    GameObject say;
    GameObject human;

    // �ź��� �˻� ��ư�� Ŭ������ ���
    public void ClickButton()
    {
        String[] sayText = { "���� �� �����ϴµ�? ĩ.. �� ����! �� ����̳� ��!", "�ƴ� �󱼺��� �� �𸣳�?", "������...", "ȣȣ~ ���� ������̱� �Ѵ�����~ ��~! �����~" };
        product = GameObject.Find("Product");
        say = GameObject.Find("SayText");
        human = GameObject.Find("Human");

        String sprite = product.GetComponent<SpriteRenderer>().sprite.ToString();

        // �̼����� ���� �Ұ� ��ǰ�� ���
        if (sprite.Equals("object1 (UnityEngine.Sprite)") || sprite.Equals("object2 (UnityEngine.Sprite)"))
        {
            // ��ǳ�� ����
            say.GetComponent<SayTextScript>().RandomSaid(sayText);
            
            // �ź��� ������
            Invoke("HumanCardSetActive", 4.0f);

            GameObject.Find("EventSystem").GetComponent<CalCulateCheckScript1>().calY.SetActive(true);
            GameObject.Find("EventSystem").GetComponent<BackScript>().backBtn.SetActive(true);
        }

        else
        {
            // ��ǳ�� ����
            String[] sayText1 = { "�� ��! �˰ڽ��ϴ�! ((�ź��� �˻簡 �ʿ��Ѱ�...?", "��? ��... �˰ڽ��ϴ�? ((���� ��°ǵ� �ź��� �˻簡 �ʿ��ϴٰ�?" };
            say.GetComponent<SayTextScript>().RandomSaid(sayText1);

            // �ź��� ������
            Invoke("HumanCardSetActive", 4.0f);

            // �մ��� sprite�� ������ ������ �ź��� ������
            String humanScript = human.GetComponent<SpriteRenderer>().sprite.ToString();

            int humanScriptNumber = (int.Parse(Regex.Replace(humanScript, @"\D", "")) - 1) / 2;

            GameObject.Find("EventSystem").GetComponent<HumanCardScript>().humanCard.GetComponent<SpriteRenderer>().sprite = GameObject.Find("EventSystem").GetComponent<HumanCardScript>().sprites[humanScriptNumber];

            GameObject.Find("EventSystem").GetComponent<CalCulateCheckScript1>().calY.SetActive(true);
            GameObject.Find("EventSystem").GetComponent<BackScript>().backBtn.SetActive(true);
        }
    }

    public void HumanCardSetActive()
    {
        GameObject.Find("EventSystem").GetComponent<HumanCardScript>().humanCard.SetActive(true);
        
    }
}
