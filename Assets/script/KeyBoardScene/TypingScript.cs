using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using TMPro;
using Random = UnityEngine.Random;
using System.Runtime.ExceptionServices;

public class TypingSpeed : MonoBehaviour
{
    public TMP_InputField userInputField;
    public TMP_Text speedDisplay;
    public TMP_Text maxSpeedDisplay;
    public string targetText = "Type this sentence!"; // ���� ������ ����

    private float startTime;
    private float maxSpeed = 0f;
    private int totalChars = 0;
    private int errors = 0;
    private const int errorPenalty = 50;
    private const float decayRate = 0.001f;
    private const float updateInterval = 0.5f;
    private float lastUpdateTime = 0f;
    private int cnt = 0;

    String updateText;
    String updateAfterText;

    String[] text = {
        "�ʸ� ���� �� õ���̾�. �Ϻ��� Ÿ�ֿ̹� �� ������ �Ǽ� �⻵",
"�ʸ� ����ϵ� ��θ� �׸� �� �� �ִٸ� ������ ����ϱ� ���� ���� �ž�.",
"���� �ʸ� ����ϰ� ���� ��ó�� �ʵ� �ʸ� �׷��� �������� ���ھ�.",
"��� �״�� ����༭ ������.",
"���� ����� �� �������� �����Դϴ�.",
"��ư��鼭 �ʹ� �ʰų�, �̸� �� ����, ���� �̷�� �� ���ѽð��� ����.",
"���� �������� �ʾ�, �������� �� ������ �ڱ� �ڽ�����.",
"������ ����� �ִ�. �幮 ���� �� ����� �̲��� ���� ������ ���� �� ����.",
"��ȹ�� ����! ��ȹ��� �ȵǴ� �� �λ��̶��ž�.",
"���� ���� ����. ������ �����̶� �ϵ���. �������ζ�.",
"��� �÷��� �ᱹ���� �ູ�� �Ǳ� �����̴�.",
"������ �����ȴ�. ������ �����ȴ�. �� ���� ����� �ǰ��� ����.",
"�ƹ��͵� �õ��� ��⸦ ���� ���Ѵٸ� �λ��� ��ü �����̰ڴ°�?",
"�̷��� �ڽ��� ���� ���� �Ƹ��ٿ��� �ϴ� ���� ���̴�.",
"���� ���� �� ����Ҽ��� ���� �� �������ٴ� �� �߰��ߴ�.",
"����� �λ����� ���� �Ǹ��� ġ������.",
"�츮�� �λ��� �츮�� ����� ��ŭ ��ġ�� �ִ�.",
"������ ����� ������ ������ ������ ������ �ϴ� ���̴�.",
"�쿬�� �ƴ� ������ ����� �����Ѵ�.",
"���� �ູ�ϰ� �ϴ� ���� ����� �Ѹ��� �Ͱ� ����.",
"�Ѹ� �� �ڱ⿡�Ե� �� ��� ������ ���� �����̴�.",
"�����ϰ� �������� �µ��� �� � Ưȿ�ຸ�� �� ���� ������ ����� ����.",
"������ �ð��� ���� ��ȸ�� �� ū �ð��� �����̴�.",
"���� �ּ��� ���ϸ� �̷��� �˾Ƽ� �� Ǯ�� ���̴�.",
"�λ��� ������ �װ��� �����ϴ� ����� ���ݴ� ��, �װ��� �ٷ� ������.",
"�ູ�� �ۿ��� ���� �ʴ´�. �ູ�� �츮�� �����ӿ��� �췯����.",
"�ູ�̶� ���� �ǹ����� �����̿�, �ΰ� ������ ��ü�� ��ǥ���� ���̴�.",
"�λ��� �帧�� ���Ѻ��� ���� �� �ӿ� �پ����.",
"���� �ڽ��� �η����ϴ� ���� �ϴ� ���̴�. �� �η����� ������ ��⵵ ����."
    };

    void Start()
    {
        targetText = text[cnt];
        GameObject.Find("game_input").GetComponent<TMP_Text>().text = targetText;
        userInputField.text = ""; // ����� �Է� �ʵ� ���
        userInputField.Select(); // ����� �Է� �ʵ� ����
        speedDisplay.text = "0"; // �ʱ� Ÿ�� �ӵ� ǥ��
        maxSpeedDisplay.text = "0"; // �ʱ� �ְ� Ÿ�� �ӵ� ǥ��
        startTime = Time.time; // ���� �ð� ���

        StartCoroutine(DecaySpeed());
    }

    void Update()
    {

        float totalTime = Time.time - startTime; // �� �ɸ� �ð� ���
        float deltaTime = Time.time - lastUpdateTime;

        if (deltaTime > updateInterval)
        {
            lastUpdateTime = Time.time;

            if (userInputField.text.Length > totalChars && userInputField.text.Length <= targetText.Length)
            {
                totalChars = userInputField.text.Length;
                float charsPerMinute = CalculateCPM(totalChars, totalTime);
                speedDisplay.text = Mathf.Max(Mathf.RoundToInt(charsPerMinute), 0) + ""; // CPM ǥ��

                if (charsPerMinute > maxSpeed)
                {
                    maxSpeed = charsPerMinute;
                    maxSpeedDisplay.text = Mathf.Max(Mathf.RoundToInt(maxSpeed), 0) + ""; // �ְ� CPM ǥ��
                }
            }
        }

        if (totalTime > 0)
        {
            totalChars -= (int)(decayRate * Time.deltaTime); // Time.deltaTime�� ����Ͽ� 1�ʸ��� 1�� ����
            totalChars = Mathf.Max(totalChars, 0); // ���� ����
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (userInputField.text.Length >= targetText.Length)
            {
                if(cnt == 27)
                {
                    GameObject.Find("EventSystem").GetComponent<EndingScript>().SetActiveObject();
                }

                userInputField.text = userInputField.text.Substring(0, userInputField.text.Length - 2) + userInputField.text.Substring(userInputField.text.Length - 2).Replace(" ", "");
                Debug.Log(userInputField.text);
                Debug.Log(targetText);

                if (userInputField.text != targetText)
                {
                    GameObject.Find("EventSystem").GetComponent<EndingScript>().heart--;

                    if(GameObject.Find("EventSystem").GetComponent<EndingScript>().heart == 2)
                    {
                        GameObject.Find("heart (2)").SetActive(false);
                    }

                    else if(GameObject.Find("EventSystem").GetComponent<EndingScript>().heart == 1)
                    {
                        GameObject.Find("heart (1)").SetActive(false);
                    }

                    else
                    {
                        GameObject.Find("heart").SetActive(false);
                        GameObject.Find("EventSystem").GetComponent<EndingScript>().SetActiveObject();
                    }
                }

                GameObject.Find("EventSystem").GetComponent<ArrowScript>().Arrow();

                cnt++;
                targetText = text[cnt];
                GameObject.Find("game_input").GetComponent<TMP_Text>().text = targetText;
                userInputField.text = "";
                userInputField.Select();
                userInputField.ActivateInputField();
                startTime = Time.time;
                totalChars = 0;
                errors = 0;
            }
        }

        if (userInputField.text.Length <= targetText.Length && userInputField.text.Length != 0)
        {
            updateText = "";
            for (int i = 0; i < userInputField.text.Length; i++)
            {
                if (userInputField.text[i] == targetText[i])
                {
                    SetTextColor(i, Color.black);
                }
                
                else
                {
                    SetTextColor(i, Color.red);
                }

            }
            GameObject.Find("game_input").GetComponent<TMP_Text>().text = updateText + updateAfterText;
        }
    }

    void SetTextColor(int index, Color color)
    {
        updateText += $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{targetText[index]}</color>";
        updateAfterText = targetText.Substring(index + 1);
    }

    float CalculateCPM(int characters, float time)
    {
        float adjustedChars = characters - (errors * errorPenalty);
        float charsPerMinute = adjustedChars / (time / 60);
        return charsPerMinute + 200;
    }

    IEnumerator DecaySpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            totalChars -= 3;
            totalChars = Mathf.Max(totalChars, 0);
        }
    }
}
