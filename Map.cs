using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//BackGround에서 사용할 예정
public class Map : MonoBehaviour{
    
    private Dictionary<int, string> mapList;
    private MapEnter mapEnter;
    [SerializeField] public GameObject creatureLayer; //생명체 감지
    private BoxCollider2D areaCollider; //생명체의 position을 받는게 아닌 적을 탐지할 영역의 정중앙을 이용하기 위해 사용
    // private Vector2 colliderCenter;
    [SerializeField] public List<GameObject> creatrue;
    
    // [SerializeField]
    

//  map의 테마를 결정하는 것 -> 자연, 숲 등과 같은 것을 결정하는 단계
    
//start, fixedUpdated, isMobcheck가 오류
    
    // private void Start(){ //ui표시 
    //     mapEnter = gameObject.AddComponent<MapEnter>();
    //     creatrue = new List<GameObject>();
    //     if(true){ //나중에 맵 이동했을 때나, 시작 ui눌러 졌을 때 실행되게
    //         mapEnter.MapChange(); //이 부분이 오류가 있다?
    //     }
    //     //mapList = new Dictionary<int, int>();
    //     // colliderCenter = new Vector2(areaCollider.bounds.center.x, areaCollider.bounds.center.y);
    //     List<GameObject> UI = new List<GameObject>();
    //     StartCoroutine(mapEnter.loadScene(2));
    // }

    private void Start()
    {
        // mapEnter 객체 초기화, 같은 오브젝트에 존재하는지 확인
        mapEnter = GetComponent<MapEnter>();
        if (mapEnter == null)
        {
            mapEnter = FindObjectOfType<MapEnter>();
        }

        // 생명체 리스트 초기화
        // creatrue = new List<GameObject>();

        // // 맵 리스트 초기화
        // mapList = new Dictionary<int, string>();

        // areaCollider와 creatureLayer 확인 및 초기화
        if (creatureLayer != null)
        {
            areaCollider = creatureLayer.GetComponent<BoxCollider2D>();
            if (areaCollider == null)
            {
                Debug.LogWarning("BoxCollider2D가 creatureLayer에 연결되지 않았습니다.");
            }
        }
        else
        {
            Debug.LogWarning("creatureLayer가 설정되지 않았습니다.");
        }

        // 맵 변경 함수 실행 (맵 이동 또는 시작 시 실행)
        if (true)  // 나중에 조건 추가 필요
        {
            mapEnter.MapChange(); // MapChange 호출
        }

        // UI 리스트 초기화 (여기서는 빈 리스트로 사용)
        // List<GameObject> UI = new List<GameObject>();

        // Scene 비동기 로드
        StartCoroutine(mapEnter.loadScene(mapEnter.mapNum));
    }


    private void Awake(){
        mapEnter = GetComponent<MapEnter>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            // 플레이어가 트리거 영역에 들어왔을 때 실행할 코드
            Debug.Log("Enemy is come in!");
            GameObject inObject = new GameObject();
            creatrue.Add(inObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Enemy")) {
            Debug.Log("Enemy is dead!");
            GameObject outObject = new GameObject();
            creatrue.Remove(outObject);
        }
    }
}

