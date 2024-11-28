using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

//아마 map이동 포탈 등에서 pressed하면 이동하는 모듈
public class MapEnter : MonoBehaviour{
    [SerializeField] public bool enterPressed;

    [SerializeField] public GameObject portal;
    
    private Animator anim; //열리는 anime를 나타내는 것에 이용함
    //public Animation door_close;//닫히는 anime를 나타내는 것에 이용
    
    private Map map;
    
    [SerializeField] public int stageNum; //3스테이지 정도, id용도
    
    [SerializeField] public int roundNum; //5라운드 정도, id용도
    
    private List<string> mapCheck = new List<string>();
    
    [SerializeField] public List<GameObject> UI; //복사할 ui들
    
    [SerializeField] public GameObject Player; //복사할 player
    
    [SerializeField] public Vector2 playerStartPosition;
    
    [SerializeField] public AsyncOperation sceneAsync;

    [SerializeField] public string mapNum; //SelectMap1, SelectStage에서 나온 변수의 값을 string으로 입력받을 것
    private List<GameObject> colliders;
    private List<int> mapCode;
    
    
    // private void Start(){ //ui표시
    //     if (SceneManager.GetActiveScene().name == "MainMenu"){
    //         return;
    //     }

    //     map = gameObject.AddComponent<Map>();
    //     enterPressed = false;
    //     colliders = null;
    //     anim = portal.GetComponent<Animator>();

    //     sceneAsync = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    //     sceneAsync.allowSceneActivation = false;

    // }

//     
    private void Awake(){
        var mapCode = new List<int>[]
        {
            new List<int> { 1, 2, 3, 4, 5 },
            new List<int> { 6, 7, 8, 9, 10 },
            new List<int> { 11, 12, 13, 14, 15 }
        };

    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") return;
        map = FindObjectOfType<Map>();
        enterPressed = false;
        

        if (portal != null) anim = portal.GetComponent<Animator>();
        else Debug.Log("Portal GameObject가 할당되지 않았습니다."); //이게 정상임

        sceneAsync = SceneManager.LoadSceneAsync("1-1", LoadSceneMode.Additive);
        sceneAsync.allowSceneActivation = false;

        if (UI == null || UI.Count == 0) Debug.LogWarning("UI 리스트가 비어있습니다.");
        if (Player == null) Debug.LogWarning("Player GameObject가 할당되지 않았습니다.");
    }

    private void SelectMap(){ 
        stageNum = Random.Range(0, 3);
        switch(stageNum){
            case 0:

            case 1:

            case 2:
        }  
    }

    private void SelectStage(int stageNum){ 
        roundNum = Random.Range(0, 5);
    }
    /*
    private void 
    switch(stageNum){
        case 0:
            

        case 1:


        case 2:
    }
    */

    public void MapChange()
    {
        do
        {
            SelectMap(); SelectStage();
            mapNum = $"{stageNum + 1}-{roundNum + 1}";
        } while (mapCheck.Contains(mapCode[stageNum][roundNum]));
        Debug.Log($"Selected Map: {mapNum}");

        if (enterPressed)
        {
            mapCheck.Add(mapNum);
            SceneManager.LoadScene(mapNum);
        }
    }

    public IEnumerator loadScene(string sceneName)
    {
        sceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        sceneAsync.allowSceneActivation = false;

        while (sceneAsync.progress < 0.9f)
        {
            Debug.Log($"Loading scene {sceneName} Progress: {sceneAsync.progress}");
            yield return null;
        }
        if (enterPressed) OnFinishedLoadingAllScene();
    }

    public void enableScene(string sceneName)
    {
        sceneAsync.allowSceneActivation = true;
        Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);

        if (sceneToLoad.IsValid())
        {
            foreach (GameObject uiElement in UI)
            {
                if (uiElement != null) SceneManager.MoveGameObjectToScene(uiElement, sceneToLoad);
            }

            if (Player != null)
            {
                SceneManager.MoveGameObjectToScene(Player, sceneToLoad);
                Player.transform.position = playerStartPosition;
            }

            SceneManager.SetActiveScene(sceneToLoad);
        }
    }

    void OnFinishedLoadingAllScene()
    {
        Debug.Log("Done Loading Scene");
        enableScene(mapNum);
        Debug.Log("Scene Activated!");
    }

    private void Update()
    {
        colliders = map.creatrue;
        SpawnPortal();
        StartCoroutine(DelayedMapChange());
    }

    private IEnumerator DelayedMapChange()
    {
        Enter();
        yield return new WaitForSeconds(5);
        MapChange();
    }

    private void Enter()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enterPressed = true;
            anim.SetBool("open", false);
            anim.SetBool("close", true);
            portal.SetActive(false);
        }
        else
        {
            enterPressed = false;
            portal.SetActive(false);
            anim.SetBool("open", true);
            anim.SetBool("close", false);
        }
    }

    private void SpawnPortal()
    {
        if (colliders.Count == 0)
        {
            portal.SetActive(true);
            anim.SetBool("open", true);
            anim.SetBool("close", false);
        }
        else
        {
            portal.SetActive(false);
            anim.SetBool("open", false);
            anim.SetBool("close", false);
        }
    }

}