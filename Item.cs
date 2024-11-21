using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; //나중에 아이템 표시 장치로 이용


//위에서 아이템이 계속해서 떨어지는 것으로 함
//이 아이템은 자동으로 먹어지는 아이템
//다른 아이템에서는 수동으로 먹어야 함 -> 아마 특수 무기 등이 나올듯
public class Item : MonoBehaviour
{
    private int item_num; //경험치 같은 것(랜덤으로 생성)
    [SerializeField]
    private Rigidbody2D itemrb;
    private float time;
    private Player player;
    private Enemy enemy;
    private int reloadNum = 2;
    // Start is called before the first frame update
    void Start()
    {
        itemrb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player"){
            random_item();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy" && Time.deltaTime > 8){
            Destroy(gameObject); //적한테 부딪히면 그냥 삭제
        }
    }

    

    public void random_item(){ //일종의 랜덤 아이템 -> 일단 player debuff
        item_num = Random.Range(0, 200); //이 item_num은 나중에 enemy에 있는 lucky로 바꿀 거임
        if(item_num < 100){
            buff(); //0부터 99까지
        }

        else{
            debuff(); //100부터 199까지
        }
    }

    void buff(){
        if(item_num <= 33 && item_num >= 1){
            player.player_damage *= 2f;
        }
        else if(item_num <= 66 && item_num >= 34){
            player.player_hp *= 2f;
        }
        else if(item_num <= 99 && item_num >= 67){

        }
        else if(item_num == 0){ //효과 없음
            
        }
    }

    void debuff(){
        if(item_num <= 133 && item_num >= 101){
            player.player_damage *= 0.5f;
        }
        else if(item_num <= 166 && item_num >= 134){
            player.player_hp *= 0.5f; 
        }
        else if(item_num <= 199 && item_num >= 167){

        }
        else if(item_num == 100){ //효과없음
            
        }
    }

/*    void Itme_timer(){ //추가할 건지 고민

    }
*/

    private void reload(){ //아이템 한, 두번 정도는 바꿀 수 있는 기회를 주는 것
        if(reloadNum > 0){
            random_item();
            reloadNum--;
        }
    }
}

