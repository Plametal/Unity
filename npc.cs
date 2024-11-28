using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc : MonoBehaviour
{
    [SerializeField]
    public int npcId;
    [SerializeField]
    public Text talkText; //각 id마다 말하는게 다름, js나 html로 작성할 수 있으면 그걸로 변경
    private bool keyDown;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    public GameObject npcGameObject;
    private GameObject tButton;
    private Animator tButtonAnime; //t버튼이 움직이는 그림을 만듦

    private void Awake() {
        tButton = GameObject.Find("Canvas/tButton"); //나중에 애니메이션 넣음  
    }
    private void Update(){
        if(keyDown){
            talker(npcId, talkText);
        }
    }
//p only if q, q when p, p is sufficient for q -> 중요, triple logic s v o, s p o, 연산자 우선순위 표
    private void KeyChecked(){
        if(Input.GetKeyDown(KeyCode.T)){
            keyDown = true;
        }
        else{
            keyDown = false;
        }
    }
//  UI에 넣는 문구
    private void talker(int npcId, Text talkText){
        switch (npcId)
        {
            case 1:
                Debug.Log("this is case1");
                string npcText = talkText.text;
                break;
            case 2:
                Debug.Log("this is case2");
                break;
            case 3:
                Debug.Log("this is case3");
                break;
            case 4:
                Debug.Log("this is case4");
                break;
            case 5: //아마 아무것도 표시 안할듯
                Debug.Log("there's nothing");
                break;
            default:
                Debug.Log("this is obstacles");
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision){ //t를 눌러라는 UI생성
        npcGameObject.layer = 11;
        while(keyDown == false){
            spriteRenderer.color = new Color(255, 255, 204, 1f);
            spriteRenderer.color = new Color(0, 0, 0, 1f); //임의로 둔 값
             //약간 연노랑색으로 바뀌게 표
        }
        tButton.transform.position = Camera.main.WorldToScreenPoint(npcGameObject.transform.position + new Vector3(0, 1.5f, transform.position.z));
    }

    // color(255, 255, 204);
    // color(255, 250, 204);
    // color(255, 255, 208);
    // color(255, 250, 205);
}