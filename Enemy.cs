using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{   
    [SerializeField]
    private GameObject target;  // 초기화하지 않고 선언 -> 이거 player 안에 넣어야 함
    // hat’s simply because the GameObject is just a container for components. 
    // It offers only little functionalities like getting / setting the layer or destroying the whole gameobject
    private bool isTracing;

    [SerializeField]
    public Transform nowPos; //이거 적 object 그냥 넣으면 됨 -> enemy 들어가는 것  
    // The Transform component offers: the position, rotation, scale of the object in the scene

    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    private Rigidbody2D enemeyrb;
    private float nextMove;
    //body는 몬스터의 데미지 판정을 받는 곳
    private BoxCollider2D body;
    //Area는 적이 player를 쫓아오는 collider
    private CircleCollider2D Area;
    
    [SerializeField]
    public float monster_hp;    
    // private float currentHP;
    [SerializeField]
    public float monster_damage = 2f;

    
    public int monster_id; //몬스터의 각 레벨을 나타내며 체력과 공격력이 바뀐다.
    
    [SerializeField]
    public string monster_who; //보스와 중간 보스, 일반병 등을 구분하는 역할

    //데미지를 받아오는 역할을 한다.
    SpriteRenderer spriteRenderer; 

    private LayerMask EnemyLayer; //레이어 선택
    private float FindRange = 6f; //범위
    [SerializeField]
    public float Enemy_EXP;
    [SerializeField]
    public int monster_level; //EnemySpawn에서 q값을 받아올거임 -> 최대 개체 수 보다 많을 경우에 가동
    [SerializeField]
    public float monsterAttackSpeed;
    private Player player;
    private Attack attack;
    public Item item;
    private SpawnEnemy spawnenemy;

    private int enemyId;
    //[SerializeField]
    //private Animator anime; //적이 움직일 때 바라보는 곳 정하는 animation
    private EnemyHpBar enemyHpBar;
    private void Awake()
    {
        enemeyrb = GetComponent<Rigidbody2D>();
        //target = new GameObject();  // 여기서 GameObject를 생성합니다.
        body = GetComponent<BoxCollider2D>();
        Area = GetComponent<CircleCollider2D>();
    }

    private void Start(){
        
        player = FindObjectOfType<Player>(); //이거 다른 class 받을 때
        //anime = GetComponent<Animator>();
        enemyHpBar = GetComponent<EnemyHpBar>(); //그냥 일반 객체들 -> gamemobject를 말함
        attack = FindObjectOfType<Attack>();
        isTracing = false;
    }

    public void randomValue(){
        enemyId = Random.Range(0, spawnenemy.random);
    }

    private void FixedUpdate() {
        Move();
        //attack.Damage()
    }
    private void Update(){ //player 추적 감지 //error
        var EnemyObj = Physics2D.OverlapCircle(transform.position, FindRange, EnemyLayer);
        //print(EnemyObj);
        if (EnemyObj != null)
        { //거리에 따른 damage에 대한 코드
            float distance = Vector2.Distance(transform.position, EnemyObj.transform.position);
            float damageMultiplier = CalculateDamageMultiplier(distance);
/*
            float damageToApply = baseDamage * damageMultiplier;
            ApplyDamageToEnemy(EnemyObj, damageToApply);
            */
        }

        else{
            Debug.Log("지금 거리 확인 못하고 있음");
        }
        
    }

    private void OnDrawGizmos() // 범위 그리기
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, FindRange);
        }
//추격기능은 나중에 다시 작성
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bullet"){
            this.monster_hp = attack.Damage(this.monster_hp, player.player_damage, player.player_attack_speed); //error
        }
        if (collision == Area && collision.gameObject.tag == "Player")
        {
            // 추적할 플레이어의 위치를 계속 업데이트 
            target = collision.gameObject;
        }
        //wall은 장애물을 나타내고 attack은 player가 공격하는 탄환(?) 같은 것을 의미
        else if(collision == body && (collision.gameObject.tag == "Attack" || collision.gameObject.tag == "wall")){
            this.monster_hp = attack.Damage(monster_hp, player.player_damage, player.player_attack_speed);
            if(monster_hp < 0){
                Die();
            }
            //Time.time은 경과한 시간, TIme.deltaTIme은 프레임 간의 시간 간격
            //프레임에 관한 작업을 할 때 deltaTIme을 사용
            // if(Time.time > 5){
            //     hpSlider.gameObject.SetActive(false);
            // }
            // UpdateHPBar();
            // attack.OnDamaged(collision.transform.position);
        }
    }

    // private void UpdateHPBar()
    // {
    //     if (hpSlider != null)
    //     {
    //         hpSlider.value = currentHP / monster_hp;
    //     }
    // }

    // 몬스터 사망 처리
    public void Die()
    {
        // 사망 처리 로직
        // 예: 몬스터 비활성화, 아이템 드롭 등
        
        enemyHpBar.monsterHpBar.gameObject.SetActive(false);
        Destroy(gameObject);
        float lucky = Random.Range(0, 10);
        if(lucky < 1){
            item.random_item();
        }
    }

    private float CalculateDamageMultiplier(float distance)
    {
        float attack_distance = 6f - distance;
        monster_damage *= attack_distance;
        return monster_damage; 
    // 거리에 따른 대미지 계산 로직을 작성하세요.
    // 예: 가까울수록 높은 값, 멀면 낮은 값 반환
    // 또는 거리에 따른 테이블을 사용할 수 있습니다.
    // 이 함수는 거리에 따른 대미지 배율을 반환합니다.
    }
/*
    private void ApplyDamageToEnemy(Collider2D enemyCollider, float damage)
    {
    // 적에게 대미지를 적용하는 로직을 작성하세요.
    // 예: 적의 스크립트에서 TakeDamage 함수를 호출하거나 직접 HP를 감소시킵니다.
    }
*//*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == Area && collision.gameObject.tag == "Player")
        {
            isTracing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTracing = false;
        }
    }
*/

    // private void OnCollisionEnter2D(Collision2D collision) {
    //     if(collision.gameObject.tag == "Block"){
    //         move().SetActive(false);
    //     }

    //     else if(collision.gameObject.tag == "BackGround");
    //     {
    //         move().SetActive(true);
    //     }
    // }

    void Move()
    {
        if (isTracing)
        {
            Vector2 playerPos = target.transform.position; //player를 의미함
            float tmp = nowPos.position.x - playerPos.x;
            if (tmp > 0)
            {
                nextMove = -1;
                // anime.SetBool("Left", true);
                // anime.SetBool("Right", false);
            }
            else if (tmp < 0)
            {
                nextMove = 1;
                // anime.SetBool("Left", false);
                // anime.SetBool("Right", true);
            }
        }

        else{
            enemeyrb.velocity = new Vector2(moveSpeed, enemeyrb.velocity.y);
            if(Time.time % 2 == 0){ //2초마다 오른쪽으로 이동
                moveSpeed *= 1;
            }
            else if (Time.time % 2 == 0) // 2초마다 왼쪽으로 이동
            {
                moveSpeed *= -1;
            }

            // OnCollisionEnter2D 메서드는 자동으로 호출됩니다.
            // 벽에 부딪히면 자동으로 왔다갔다
            void OnTriggerEnter2D(Collision2D collision)
            {
                if(collision.gameObject.tag == "Background"){
                    moveSpeed *= -1;
                }
            }

        enemeyrb.velocity = new Vector2(nextMove * moveSpeed, enemeyrb.velocity.y);
        }
    }
    

    private float monster_aim(){
        
        return monster_damage;
    }

    
    
    
   
//몬스터 공격력, hp 등을 체크하는 곳 -> 보스몹, 중간몹, 일반몹 다 다름
    public void monster_level1(int monster_id){
        if(monster_id == 1){ //근접
            this.monster_hp = 20f;
            this.monster_damage = 4f;
            this.monsterAttackSpeed = 1f;
            this.moveSpeed = 5f;
        }
        
        else if(monster_id == 2){ //원거리
            this.monster_hp = 10f;
            this.monster_damage = 2f;
            this.monsterAttackSpeed = 0.5f;
            this.moveSpeed = 2f;
        }
        
        else if(monster_id == 3){ //근접 폭발
            this.monster_hp = 5f;
            this.monster_damage = monster_aim();
            this.monsterAttackSpeed = 3f; //공격하는게 없음
            this.moveSpeed = 10f;
        }

        else if(monster_id == 4){ //?

        }

    }
    public void monster_level2(int monster_id){
        if(monster_id == 1){
            this.monster_hp *= 1.2f;
            this.monster_damage *= 1.2f;
            this.monsterAttackSpeed *= 1.2f;
            this.moveSpeed *= 1.2f;
        }
        
        else if(monster_id == 2){
            this.monster_hp *= 1.2f;
            this.monster_damage *= 1.2f;
            this.monsterAttackSpeed *= 1.2f;
            this.moveSpeed *= 1.2f;
        }
        
        else if(monster_id == 3){
            this.monster_hp *= 1.2f;
            this.monster_damage = monster_aim() * 1.2f;
            this.monsterAttackSpeed *= 1.2f;
            this.moveSpeed *= 1.2f;
        }

        else if(monster_id == 4){

        }
    }

    public void monster_level3(int monster_id){
        if(monster_id == 1){
            this.monster_hp *= 1.5f;
            this.monster_damage *= 1.5f;
            this.monsterAttackSpeed *= 1.5f;
            this.moveSpeed *= 1.5f;
        }
        
        else if(monster_id == 2){
            this.monster_hp *= 1.5f;
            this.monster_damage *= 1.5f;
            this.monsterAttackSpeed *= 1.5f;
            this.moveSpeed *= 1.5f;
        }
        
        else if(monster_id == 3){
            this.monster_hp *= 1.5f;
            this.monster_damage = monster_aim() * 1.5f;
            this.monsterAttackSpeed *= 1.5f;
            this.moveSpeed *= 1.5f;
        }

        else if(monster_id == 4){

        }

    }

    public void Boss(){
        if(monster_id == 1001){

        }
        else if(monster_id == 2001){

        }
        else if(monster_id == 3001){

        }

    }
}
    
