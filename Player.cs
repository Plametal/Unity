using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float maxSpeed;
    [SerializeField]
    public float JumpPower;
    //public GameManager manager;
    private GameObject scanObject;
    //public float heart;
    [SerializeField]
    private Rigidbody2D rb;
    //[SerializeField]
    public Animator anim;
    SpriteRenderer spriteRenderer; 
    [SerializeField]
    public float player_attack_speed;
    public Attack attack; //attack 모듈
    [SerializeField]
    public float player_hp;
    [SerializeField]
    public float player_damage;
    //private Transform Projectile;
    

    //private bool spaceKeyPressed = false;
    public Enemy enemy;
    public GameObject bullet_clone;
    private SpawnEnemy spawnEnemy;
    private bool isJumping;
    
    /*
    키 입력은 주로 Update()에서 확인하고, 해당 상태를 다른 변수에 저장합니다.
    그런 다음 FixedUpdate()에서 해당 변수를 사용하여 물리 시뮬레이션과 관련된 동작을 수행합니다.
    이렇게 분리하면 입력 상태를 안정적으로 처리하면서 물리 시뮬레이션과 동기화할 수 있습니다.
    */

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }   
    private void Start(){
        //enemy = gameObject.AddComponent<Enemy>();
        enemy = FindObjectOfType<Enemy>();
        //attack = GetComponent<Attack>();
        attack = FindObjectOfType<Attack>();
        isJumping = false;
        spawnEnemy = FindObjectOfType<SpawnEnemy>();
    }

    private void FixedUpdate(){
        move_anime();
        if(Input.GetKeyDown("mouse1")){ //나중에 입력받는 키 코드 값을 변경할 예쩡
            attack.Fire();
        }
    }

    private void Update(){
        //this.player_hp = player_hp;
    }

    //attack에만 넣을지, player에만 이런 코드를 넣을지 고민해야 함
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            this.player_hp = attack.Damage(player_hp, enemy.monster_damage, enemy.monsterAttackSpeed);
        // enemy 초기화
        // 예시: Enemy 스크립트가 적용된 오브젝트에서 Enemy 컴포넌트를 가져옴
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Attack") {
            this.player_hp = attack.Damage(player_hp, enemy.monster_damage, enemy.monsterAttackSpeed);
            //attack.OnDamaged(collision.transform.position, attack.isHit); -> ontrigger2
            
            //if(){}
            
        }
        else if (collision.gameObject.tag == "Wall") {
            //attack.OnDamaged(collision.transform.position, attack.isHit);
            //이거 onDamaged말고 그냥 다른 거 나오게 하는 걸로
            Debug.Log("Its wall");
        }
    
    }


/*
    void OnDamaged(Vector2 targetPos) {
        gameObject.layer = 11;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1; 
        rb.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        //시간 차를 주기 위해서 사용, 파라미터가 있는 함수 사용 불가능
        Invoke("OffDamaged", 1);
    }

    void OffDamaged() {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    */

    //하트 게이지
    /*
    private int heart_gaze(float heart) {
        this.heart = heart;
        return 0;
    }
    

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Projectile")){
            Attack attack = collision.GetComponent<>();
        }
    }

    

    private void attack_check() { //공격 하고 있는지 확인
        if (Input.GetKeyDown(KeyCode.Return)) { //임의로 키 설정
            // Handle attack input
        }
        else {
            isattack = false;
        }
    }

    private void discharge() { //발사
        // Handle discharge
    }
    */
  //애니메이션 수정해야 함 -> 2024/11/18 수정 시작
    private void move_anime(){
        float h = Input.GetAxisRaw("Horizontal");

        // 수평 방향 움직임 제어
        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rb.velocity.x > maxSpeed) { //오른쪽으로 이동하는 장면
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        } 
        else if (rb.velocity.x < -maxSpeed) { //왼쪽으로 이동하는 장면
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) { //점프를 시도하는 코드
            // void OnCollisionEnter2D(Collider2D collision){
            //     if(collision.gameObject.tag == "Background") break;
            //     else{
                        rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse); //점프력? 측정 코드 -> 그런데 이거 반복되는 버그가 있음 -> 공중부양
                        anim.SetBool("isJumping", true); //점프 animation툴 사용
                        isJumping = true; // 점프 상태를 true로 변경
                 //}
             //}    
        }
        // Movement and animations
        if (rb.velocity.x > 0.1f) {
            anim.SetBool("Right", true);
            anim.SetBool("Left", false);
            anim.SetBool("isWalking", false);
        }   
        
        else if (rb.velocity.x < -0.1f) {
            anim.SetBool("Right", false);
            anim.SetBool("Left", true);
            anim.SetBool("isWalking", false);
        } 
        else {
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
            anim.SetBool("isWalking", true);
        }
        
        // 레이케스트로 바닥에 닿아 있는지 확인
        Debug.DrawRay(rb.position, Vector2.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector2.down, 0.01f);
        if (rayHit.collider != null && rayHit.collider.CompareTag("Background"))
        {
            // 태그가 "YourTag"인 오브젝트와 충돌했을 때 실행할 코드
            Debug.Log("태그가 YourTag인 오브젝트를 감지했습니다!");
            anim.SetBool("isJumping", false);
            isJumping = false; // 바닥에 닿았을 때 점프 상태 해제
        }
        // 바닥에 닿으면 점프 상태 해제
    }
        //Landing platform -> 레이 케스트 작업, 레이저 같은 것
        /*Debug.DrawRay(rb.position, Vector2.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector2.down, 0.7f, LayerMask.GetMask("Object"));\
        */
}
