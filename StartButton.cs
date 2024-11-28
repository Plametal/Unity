using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Button startBt, exitBt, optionBt, loadBt;
    // Start is called before the first frame update
    void Start()
    {
        startBt.onClick.AddListener(start); //시작
        exitBt.onClick.AddListener(exit);  //종료
        optionBt.onClick.AddListener(option); //환경설정
        loadBt.onClick.AddListener(load); //이어하기
    }

    void start(){
        //scene 이동
    }

    void exit(){
        Application.Quit (); //종료
    }

    void option(){
        //환경설정 이동, 아마 일단 bgm설정만 할듯
    }

    void load(){
        //아마 load페이지 만들면 사용함 -> box형 load page
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
