using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject square;
    public Text timeTxt;
    float alive = 0f;   //지금 기록

    public Text thisScoreTxt;
    public Text maxScoreTxt;

    public Animator anim;

    bool isRunning = true;

    public static gameManager I;    //여러군데에서 불릴수 있는 게임매니저 I 가 있다.
    void Awake()
    {
        I = this;   //게임매니저를 호출하면 본인을 넣어줘라.    
    }

    public GameObject endPanel;

    public void gameOver()
    {
        isRunning = false;
        anim.SetBool("isDie", true);
        Invoke("timeStop", 0.5f); //timeStop함수를 0.5초 있다가 실행해줘.
        //Time.timeScale = 0f;    //더이상 시간이 흐르지 않게 -> 바로 0초로 바뀌기 때문에 애니메이션을 볼 시간이 없음. 위의 코드로 바꿔준다.

        endPanel.SetActive(true);   //endpanel의 상태를 다시 활성화하기
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
        Time.timeScale = 1.0f;  //시간을 다시 돌려주기
        InvokeRepeating("makeSquare", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)//true 일떄 진행, false로 바뀌면 더이상 호출x
        {
            alive += Time.deltaTime;
            timeTxt.text = alive.ToString("N2");    //소수점 둘째자리까지 표현
        }
    }

    void makeSquare()
    {
        Instantiate(square);
    }

    public void retry() //다시하기 함수
    {
        SceneManager.LoadScene("MainScene"); //메인씬을 다시 부른다.
    }
}
