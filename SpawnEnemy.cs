using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

//이제 충돌이 일어나는 일은 없음 -> 하지만 GetRandomPosition이랑 Boxcollider2d의 사이즈에 의한 적의 소환 포인트에 대해서 조정이 필요함
//맵의 크기를 넓히는 것은 가로 80~100정도로 예상, 다른 structure는 추가 생각해보기로 함 -> 어차피 background로 넣을 예정
//Item도 넣을 수 있을 지도 모름
//random 난이도 적 출력은 이 프로젝트가 beta로 종료될 경우에만 다시 수정하는 걸로, 지금은 level1만 실행되게 진행

public class SpawnEnemy : MonoBehaviour
{
    //나중에 Enemy_obj랑 Enemy는 리스트 형태로 바꾸서 할꺼임 -> 난이도 조정 목적
    
    public List<GameObject> enemyObj;
    public List<Transform> enemyForm;


    private int count = 0;
    [SerializeField]  
    public BoxCollider2D area; //spawnEnemy area -> this collider is defined where enemy is spawned;
    [SerializeField]
    public int EnemyNum =10;
    //private int q = 0; //중복 영어로 작성성
    [SerializeField]
    public int maxEnemyNum = 15;
    private RaycastHit2D rayhit;
    [SerializeField]
    private Vector2 spawnPos;
    private Enemy enemy;
    
    private int randomId;
    public int enemyId;
    private GameObject instance;
    [SerializeField]
    public List<int> destroyedIds;
    // private GameManager gameManager;
    // [SerializeField]
    private Rigidbody2D rb;
    public GameObject groundBlockPrefab;
    [SerializeField]
    //private BoxCollider2D mob; //몹 체크하는데 사용
    // [SerializeField]
    // private BoxCollider2D checker; //아마도 floorprefab이 들어갈 예정
    //[SerializeField]
    private List<Vector2> positions = new List<Vector2>();
    public int random;
    private int check; //random value를 받아 온다
    void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        area = GetComponent<BoxCollider2D>();
        // gameManager = gameObject.AddComponent<GameManager    >();
        destroyedIds = new List<int>();
        rb = GetComponent<Rigidbody2D>();
        //mob = GetComponent<BoxCollider2D>();
        //checker = GetComponent<BoxCollider2D>();
        random = RandomEnemyNum();

        for(int i = 0; i < random; i++){
            positions.Add(GetRandomPosition()); //리스트 크기가 할당되지 않았기 떄문
        }
    }


    void Update(){
        if(Time.time < 10){
            DeleteGroundBlock();
        }
    }
    
    private void CreateGroundBlock(Vector2 enemyPosition)
    {
        // 블록을 생성할 위치 리스트
        List<Vector2> blockPositions = new List<Vector2>();

        // 가운데 블록 (적의 바로 아래)
        blockPositions.Add(new Vector2(enemyPosition.x, enemyPosition.y - 1.0f));
    
        // 왼쪽 블록
        blockPositions.Add(new Vector2(enemyPosition.x - 1.0f, enemyPosition.y - 1.0f));
    
        // 오른쪽 블록
        blockPositions.Add(new Vector2(enemyPosition.x + 1.0f, enemyPosition.y - 1.0f));

        // 각 위치에 블록 프리팹을 생성
        //배열 안에 들어있는 모든 것을 똑같이 실행하기 위한 반복문
        foreach (Vector2 pos in blockPositions)
        {
            Instantiate(groundBlockPrefab, pos, Quaternion.identity);
        }
    }
    private void DeleteGroundBlock(){
        Destroy(groundBlockPrefab);
    }

    void Start(){
        Spawn(); //error
        // Box = GetComponent<BoxCollider2D>();
    }
    

    // Update is called once per frame
    void Spawn(){ //이 spawn코드가 문제
        for (int i = 0; i < random - count - 1; i++) //random만큼 책 생성 
        {   
            //spawnPos = GetRandomPosition(); //랜덤 위치 return
            spawnPos = positions[i];
            
            randomId = Random.Range(0, 3); //나중에 몬스터 id가 더 늘어나면 더 추가 -> 지금 enemy, enemy2, enemy3까지 밖에 없어서 3개까지 추가
            //원본, 위치, 이름을 매개변수로 받아 오브젝트 복제 -> 이름까지 복제하는 걸로
            //
            instance = Instantiate(enemyObj[randomId], spawnPos, Quaternion.identity); //적 생명치
            //check = enemy.randomValue(random);
            if (instance != null) 
            {
            // 스폰된 적 오브젝트에서 'Enemy' 스크립트를 가져오기
                Enemy spawnedEnemy = instance.GetComponent<Enemy>(); //enemy값 그냥 가지고 오기
                
                if(spawnedEnemy != null){
                    Debug.LogError("IDK");
                }
                
                else {
                    Debug.LogError("Spawned instance does not have the SpawnEnemy component.");
                } 
            
                if (spawnedEnemy != null) 
                {
                    // 적의 레벨 설정
                    spawnedEnemy.monster_level1(randomId);
                } 
                else 
                {
                    Debug.LogError("Spawned instance does not have the Enemy component.");
                }
            } 
            else 
            {
                Debug.LogError("Enemy_obj 리스트가 비어있습니다!");
            }

    
            // instance.GetComponent<SpawnEnemy>().enemyId = i; 
            // enemy.monster_level1(randomId);
            // gameManager.
            //충돌 감지 판정 후에, 만약 충돌하지 않는다면 설치
            Debug.DrawRay(spawnPos, Vector2.down * 0.2f, Color.green);
            if (!Physics.Raycast(spawnPos, Vector2.down, 0.2f)) {
                CreateGroundBlock(spawnPos); // You may need to adjust this if CreateGroundBlock expects a Transform
            }

        }

        while (maxEnemyNum - random > 0) //level1
        {
            //if(check){
                // int destroyId = Random.Range(0, random);

                // // 이미 제거된 아이디인 경우 다시 랜덤한 아이디를 선택
                // while (destroyedIds.Contains(destroyId))
                // {
                //     destroyId = Random.Range(0, random);
                // }

                // // 적의 ID를 기록
                // destroyedIds.Add(destroyId);

                // if (/*instance.GetComponent<SpawnEnemy>().enemyIdenemy.randomValue() */ check == destroyId)
                // {
                //     Destroy(instance);
                // }

                instance = Instantiate(enemyObj[randomId], spawnPos, Quaternion.identity);
                enemy.monster_level1(randomId); //아마 monster_level함수에서 색 변경을 참조할 것 같음
                count--;
            //}
        
        }
        
        while (random - maxEnemyNum > 0 && random - 2 * maxEnemyNum <= 0) //level 2
        {
            //if(check){
                // int destroyId = Random.Range(0, random);

                // // 이미 제거된 아이디인 경우 다시 랜덤한 아이디를 선택
                // while (destroyedIds.Contains(destroyId))
                // {
                //     destroyId = Random.Range(0, random);
                // }

                // // 적의 ID를 기록
                // destroyedIds.Add(destroyId);

                // if (/*instance.GetComponent<SpawnEnemy>().enemyIdenemy.randomValue() */ check == destroyId)
                // {
                //     Destroy(instance);
                // }

                instance = Instantiate(enemyObj[randomId], spawnPos, Quaternion.identity);
                enemy.monster_level2(randomId);
                count--;
            //}
        }

        while(random - 2 * maxEnemyNum > 0){ //level3
            //if(check){
                // int destroyId = Random.Range(0, random);

                // // 이미 제거된 아이디인 경우 다시 랜덤한 아이디를 선택
                // while (destroyedIds.Contains(destroyId))
                // {
                //     destroyId = Random.Range(0, random);
                // }

                // // 적의 ID를 기록
                // destroyedIds.Add(destroyId);

                // if (/*instance.GetComponent<SpawnEnemy>().enemyIdenemy.randomValue() */ check == destroyId)
                // {
                //     Destroy(instance);
                // }

                instance = Instantiate(enemyObj[randomId], spawnPos, Quaternion.identity);
                enemy.monster_level3(randomId);
                count--;
            //}
        }   
    }

    Vector2 GetRandomPosition()
    {

        const int maxAttempts = 100; // 반복 최대 시도 횟수
        int attempts = 0;

        do
        {
            // 기준 위치와 무작위 범위를 설정
            Vector2 basePosition = (Vector2)transform.position;
            float posX = Random.Range(-AreaSize().x, AreaSize().x);
            float posY = Random.Range(-AreaSize().y, AreaSize().y);

            // 스폰 위치 계산
            spawnPos = basePosition + new Vector2(posX, posY);

            // 충돌 체크: OverlapCircle로 특정 태그를 가진 객체와 충돌 여부 확인
            Collider2D hitCollider = Physics2D.OverlapCircle(spawnPos, 0.01f, LayerMask.GetMask("Default"));

            // 충돌한 객체의 태그 확인
            if (hitCollider == null || hitCollider.CompareTag("Background"))
            {
                // 충돌하지 않거나 허용된 태그와 충돌한 경우 종료
                break;
            }

            attempts++;
        } while (attempts < maxAttempts);

        if (attempts == maxAttempts)
        {
            Debug.LogWarning("Max attempts reached! Could not find a valid position.");
        }

        return spawnPos;
    }

    Vector2 AreaSize()
    {
        // BoxCollider2D의 크기를 계산
        area = GetComponent<BoxCollider2D>();
        if (area != null)
        {
            return area.size / 2f; // 크기의 절반 값을 반환
        }
        else
        {
            Debug.LogError("BoxCollider2D not found!");
            return Vector2.zero;
        }
    }


    //난이도 설정 등
    public int RandomEnemyNum()
    {
        random = Random.Range(5, EnemyNum); //적이 총 얼마인지
        if (maxEnemyNum - random < 0)
        {
            this.count += (maxEnemyNum - random); //최대 생성 적 수 보다 많을 경우, level up으로 간다
            

            
            //level_UP(q, random); 
            //-> 랜덤 위치에 있는 적 레벨업, 중복 레벨업은 불가능
            //나중에 UI로 몇 명이 몇 레벨이고 소환 횟수, 적 죽으면 죽은 숫자도 표시
            //Enemy source code와 연결될 예정
        }
        return random;
    }

     //q는 초과한 몬스터 수, maxEnemyNum은 생성된 몬스터 숫자 ->이유는 이건 초과인 경우에만 작용
    /*
    void level_UP(int q, int maxEnemyNum)
    {
        //...
    }
    */ 
    //나중에 제작

    // private Bool colliderCheck(){ //각각의 object들이 collision판정을 일으키면 다시 위치변경 하는 코드
    //     bool check = false; //지금은 충돌하고 있지 않은 상태다.
    //     if(mob)
    // }

    // private void OnCollisionEnter2D(Collider2D collision){ //floorprefab과 mobObject간의 boxcollider를 확인하는 역할
    //     isCollision = false;
    //     if(collision == mob && collision == checker){
    //         isCollision == true;
    //     }

    //     else{
    //         isCollision = false;
    //     }
    // }

    

    // void check(){
    //     if(isCollision){
    //         destroyedIds.Add(destroyedId); //갑자기 생각이 안남 -> subtract해야 함

    //     }
    // }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if (other.gameObject.activeSelf)
    // }
}