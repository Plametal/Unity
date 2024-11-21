using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GameObject;

/*
using static 지시문을 사용하여
GameObject 타입을 직접 가져올 수 있습니다.
*/

public class Attack : MonoBehaviour
{
    public float damage; //공격 무기 데미지
    //[SerializeField]
    public float Attack_speed; //공격 무기 속도
    public GameObject bulletPrefab;     // 발사할 총알 프리팹
    public Transform firePoint;         // 총알이 발사되는 위치
    private float speed;
    private Enemy enemy;
    private Player player;
    private float result;
    //private bool isattack = false
    // [SerializeField]
    // private Transform shot;
    private SpriteRenderer spriteRenderer;
    //private GameObject projectile;
    // [SerializeField]
    // private Rigidbody2D clone;
    //public bool isPressed; //키를 눌렀을 때 반응
    public bool isHit; 
    private Vector2 point;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        bulletPrefab = GetComponent<GameObject>();
        firePoint = GetComponent<Transform>();
        //isPressed = false; //키를 눌렀을 때 반응
        isHit = false;
    }

    // Update is called once per frame
    /*
    void Update()
    {
        if(Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")){
            clone = Instantiate(rb, shot.position, Quaternion.identity);
            
        }   
    }
    */
    // private void FixedUpdate() // 마우스 위치를 표현
    // { 
    //     // 마우스 위치를 월드 좌표로 변환하여 point에 저장
    //     point = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    // }

    

    public void Fire()
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // 총알 인스턴스 생성 및 방향 설정
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {   
            rb.velocity = direction * attackSpeed(speed) * Direction(speed); // 총알을 마우스 방향으로 발사, 각각 스피드 함수, 방향 함수
        }
    }

    // public void Fire()
    // {
    //     // mrb는 Rigidbody2D이므로 해당 GameObject를 통해 Instantiate를 호출해야 합니다.
    //     GameObject bullet = Instantiate(mrb.gameObject, shot.position, Quaternion.identity);
    //     Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>(); 
    
    //     // bullet_rb로 force 추가
    //     bullet_rb.AddForce(new Vector2(mrb.velocity.x, 0)); //속도
    //     //특정 위치로 날아가는 것
    // }

    // private void OnTriggerEnter2D(Collider2D collision) {
    //     if(collision.gameObject.tag == "Enemy"){
    //         enemy.OnTriggerEnter2D(collision);
    //         Destroy(gameObject);
    //         isHit = true; //-> enemy한테는 딱히 필요 없을듯, 아마 몬스터끼리 맞은 경우는 고민해야 함
    //     }   
        
    //     else if(collision.gameObject.tag == "Player"){
    //         player.OnTriggerEnter2D(collision); //다른 함수로 바꾸기
    //         Destroy(gameObject);
    //         isHit = true; //무적 판정을 위해서 사용됨
    //     }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
            
            if (enemyRb != null) {
                // Calculate the push direction based on the relative position of the bullet and the enemy
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                enemyRb.AddForce(pushDirection * 7, ForceMode2D.Impulse); // Adjust force as needed
            }
            
            // Destroy bullet after hitting the enemy
            Destroy(gameObject);
            isHit = true;
        } 
        // Other collision cases follow here
    

    
/*      나중에 추가 예정
        else if(collision.gameObject.tag == "wall" && (collision = "Player" || collision = "Enemy")){ //나중에 벽이 부서지는 설정도 추가할 예정 -> 이것도 조건문으로 구성
            wall.OnCollisionEnter2D(); 
            Destroy(gameObject);  
        }
*/
        else if(collision.gameObject.CompareTag("Background")){
            //if(collision == player || collision == enemy){
                 Destroy(gameObject);  
            
        }
    }
    public float Damage(float hp, float damage, float speed){ //적이든 누군가의 체력을 나타냄
        attackSpeed(speed);
        result = hp - damage;
        return result; //지금 현재 체력을 받을 때 사용하는 것
    }

    public Vector2 Direction(float speed)
    {
        // 발사체와 point 사이의 방향 벡터를 구함
        Vector2 direction = ((Vector2)point - (Vector2)bulletPrefab.transform.position).normalized;

        // 방향 벡터에 속도를 곱해 발사체의 속도를 설정
        
        return direction;
    }

    public float attackSpeed(float speed){
        this.speed = speed;

        return speed;
    }


/*
    public float attacker(float damage){
        this.damage = damage;
        return damage;
    }
*/
    // public void OnDamaged(Vector2 targetPos, bool isHit){
    //     if(isHit == true && Time.deltaTime < 0.3f){
    //         continue; //무적
    //     }

    //     else{ //그냥 아무 상황도 아닐 때, 타격한지 0.3초 이후
    //         gameObject.layer = 11;
    //         spriteRenderer.color = new Color(1, 1, 1, 0.4f);

    //         int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1; 
    //         bulletPrefab.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
    //         //시간 차를 주기 위해서 사용, 파라미터가 있는 함수 사용 불가능
    //         Invoke("OffDamaged", 0.5f);
    //     }
    // }

    // void OffDamaged(){
    //     gameObject.layer = 10;
    //     spriteRenderer.color = new Color(1, 1, 1, 1);
    //     isHit = false;
    // }

    // // public void IsHit(){ //맞았으면 피격 1회 정도 피격 불가능
    // //     if(isHit){
    // //         isHit = true;
    // //     }

    // //     else if(!isHit && Time.deltatime > 0.5f){
    // //         isHit = false;
    // //     }
    // // }
    // public void Invincibility(){ //피격당했을 시에 1초 내로 무적 현상

    // }
}
