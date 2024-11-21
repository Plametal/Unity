using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//stage설정할 때 사용할 예정
public class GameManager : MonoBehaviour
{
    
    public GameObject groundBlockPrefab;
    
    private SpawnEnemy spawnEnemy;

    private BoxCollider2D Box;
//  적이 공중에 생성될 시에 블럭을 아래에 생성해주는 코드

    private void Start(){
        spawnEnemy = gameObject.AddComponent<SpawnEnemy>();
        Box = GetComponent<BoxCollider2D>();
    }
   

    private void Update(){
        
    }


    


    

    

    
    //한 10초 이내로 밑에 있는 blcok이 다 사라질 예정
    
}

/*
    남은 작엄
    1.enemy, player animation 수정, 관행
    2.적 보스 페이지 제작 -> 일단 보스는 적이랑 그냥 일반 적과 똑같이 생긴 걸로 하고 적의 level난이도가 높아질 때는 색깔이 조금 변하는 걸로만 하자
    -> 1.단계 기본, 2단계는 약간 초록색 3단계는 그냥 초록색
    3.player 인터페이스 제작
    4.enemy 체력바 enemy rb.up위에 제작
    5.player, enemy attack 코드 습득 -> player는 누를 떄 마다, enemy는 자동으로 범위 안에 들어오게 되면 실행
*/