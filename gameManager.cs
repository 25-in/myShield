using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject square;
    public Text timeTxt;
    float alive = 0f;   //���� ���

    public Text thisScoreTxt;
    public Text maxScoreTxt;

    public Animator anim;

    bool isRunning = true;

    public static gameManager I;    //������������ �Ҹ��� �ִ� ���ӸŴ��� I �� �ִ�.
    void Awake()
    {
        I = this;   //���ӸŴ����� ȣ���ϸ� ������ �־����.    
    }

    public GameObject endPanel;

    public void gameOver()
    {
        isRunning = false;
        anim.SetBool("isDie", true);
        Invoke("timeStop", 0.5f); //timeStop�Լ��� 0.5�� �ִٰ� ��������.
        //Time.timeScale = 0f;    //���̻� �ð��� �帣�� �ʰ� -> �ٷ� 0�ʷ� �ٲ�� ������ �ִϸ��̼��� �� �ð��� ����. ���� �ڵ�� �ٲ��ش�.

        endPanel.SetActive(true);   //endpanel�� ���¸� �ٽ� Ȱ��ȭ�ϱ�
        thisScoreTxt.text = alive.ToString("N2");

        if (PlayerPrefs.HasKey("bestScore") == false)
        {
            PlayerPrefs.SetFloat("bestScore", alive);
        }
        else
        {
            if (PlayerPrefs.GetFloat("bestScore") < alive)
            {
                PlayerPrefs.SetFloat("bestScore", alive);
            }
        }
        float maxScore = PlayerPrefs.GetFloat("bestScore");
        maxScoreTxt.text = maxScore.ToString("N2");
    }

    void timeStop()
    {
        Time.timeScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;  //�ð��� �ٽ� �����ֱ�
        InvokeRepeating("makeSquare", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)//true �ϋ� ����, false�� �ٲ�� ���̻� ȣ��x
        {
            alive += Time.deltaTime;
            timeTxt.text = alive.ToString("N2");    //�Ҽ��� ��°�ڸ����� ǥ��
        }
    }

    void makeSquare()
    {
        Instantiate(square);
    }

    public void retry() //�ٽ��ϱ� �Լ�
    {
        SceneManager.LoadScene("MainScene"); //���ξ��� �ٽ� �θ���.
    }
}
