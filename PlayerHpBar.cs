using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

//proposition logic이 뭐야
public class PlayerHpBar : MonoBehaviour
{

    /*
    public class Player : MonoBehaviour
{
    private int player_hp = 100;

    public int PlayerHp  // 외부에서 읽기 전용
    {
        get { return player_hp; }
        private set { player_hp = value; }  // 내부에서만 변경 가능
    }
}
*/
    public Image hpBar;
    //public Text gaugeText;
    public Item item;
    public float first = 100f; //hp처음량
    public float last = 0f; //hp깍일 때마다 삽입
    public float currentValue = 0f;
    private Player player;     
    private GameObject playerHpBar;
    private Enemy enemy;

    private void Start()
    {
        StartCoroutine(GaugeAnimation(0, first, first, last));
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
        first = player.player_hp;
        playerHpBar = GameObject.Find("Canvas/playerSlider");
    }

    private void Update(){
        this.first = this.last;
        playerHpBar.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(0, 0.8f, transform.position.z));
        //player 위에도 뜨게 만들고 따로 ui창을 만들어서 할 예정 아마, 경험치 게이지 옆에 둘 예쩡
    }
//https://notyu.tistory.com/62
    private IEnumerator GaugeAnimation(float min, float max, float f, float l)
    {
        currentValue = f;
        hpBar.fillAmount = (currentValue - min) / (max - min);
            //gaugeText.text = currentValue.ToString();
            //toString은 그냥 text에 입력하는 것

            //elapsedTime += Time.deltaTime;

        yield return null;
        
    }

}
