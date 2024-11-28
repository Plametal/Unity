using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : MonoBehaviour
{
    private float Bossdamage;
    private float BossHp;
    private GameObject bomb; //보스 전용 폭탄
    private Attack attack; //플레이어가 공격한 데이미 설정
    private Animator anim; //page selection
    private Player player;
    private EnemyHpBar hpbar; //image를 사용하기 위해서
    private Image bosshpbar;
    private void Awake() {
        BossHp = GetComponent<float>();
        bomb = GetComponent<GameObject>();
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        hpbar = GetComponent<EnemyHpBar>();
    }

    private void Start(){
       // bosshpbar = AddComponent<hpbar>(Image); //hp파일 image받아오기
        BossCheck();
    }


    private void Update(){
        BossAttackSwipe(BossHp); //보스페이즈 확인하는 코드
    }

    private void BossAttackSwipe(float BossHp){ 
    //보스의 공격패턴을 진행시킨다.
        if(BossHp % 100  <= 50){ //2페이즈 (공격증가, hp증가, 공격패턴 증가 등등)
            explosion();
            //attack에 들어있는 damage함수 넣을 예정 -> 아마도 패턴이 바뀌기 보다는 능력치의 변동이 있을 예정
        }
        else{ //보스 1페이즈

        }
    }

    private void OnCollisionEnter2D(Collider2D collision){ //player에게 공격을 받았을 때, 적용
        if(collision.gameObject.tag == "Attack"){
            //Attack class에 있는 함수 사용
            attack.Damage(BossHp, player.player_damage, player.player_attack_speed);
        }

        else if(collision.gameObject.tag == "wall"){ //돌진을 할 예정
            attack.Damage(BossHp, 10, 0); //딱히 공격속도가 필요없음 -> 이건 자해대미지
        }

        else if(collision.gameObject.tag == "Player"){
            attack.Damage(player.player_hp, Bossdamage, 0);
        }

        
    }

    private void Rush(){
        
    }

    private void BossCheck(){ //보스의 기본적인 능력을 설정하는 공간, 아마 hp도 들어갈 예정
        BossHp = 300f;
        Bossdamage = 15f;
    }

    private void UI(){ 
    //보스 전용 hp bar Ui랑 다양한 ui를 제작할 예정 -> 아마 보스는 근거리도 원거리도 가능할 예정(아마 원거리는 폭탄 떨구기)

    }

    private void ThrowBomb(){ //애니메이션 설정 -> 폭탄 떨구기(하늘에서)

    }

    private void explosion(){ 
        //anim.SetBool(); 
    }










}