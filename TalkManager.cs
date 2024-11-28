using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    // //기본적인 대화만 구현이 가능
    // //여러 개의 문장을 입력받기 위해 빈 배열 사용
    // Dictionary<int, string[]> talkData;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     talkData = new Dictionary<int, string[]>();
    //     GenerateData();
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    
    // void GenerateData(){
    //     talkData.Add(1000, new string[] { "hello", "",});
    // }

    // public string GetTalk(int id, int talkIndex){
    //     return talkData[id][talkIndex];
    // }
    private Text talkText;
    private GameObject scanObject;
    private GameObject panel;
    // public bool isAction;
    // public void Action(GameObject scanObj){
        
    //     if(isAction){
    //         isAction = false;
    //     }
    //     else{
    //         isAction = true;
    //         scanObject = scanObj;

    //     }
    //     panel.SetActive(isAction);
    // }

    void Talk(){
       // talkManager.GetTalk(id, talkIndex);
    }

    private void OnCollisionEnter(Collision other) {
        if(other == null){
            //아무일도 없음 
        }
        
        else{
            //preess T라는 버튼을 UI로 생성, 아마 npc머리 위에 생성될 예정, 아이템 상점임
        }
    }
}
