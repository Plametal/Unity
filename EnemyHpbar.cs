using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

//proposition logic이 뭐야
public class EnemyHpBar : MonoBehaviour
{
    public Image hpBar;
    //public Text gaugeText;
    private Item item;
    private float first; //hp처음량
    private float last = 0f; //hp깍일 때마다 삽입
    private float currentValue = 0f;
    public Enemy enemy;
    public GameObject monsterHpBar;
    private void Start()
    {
        first = enemy.monster_hp;
        StartCoroutine(GaugeAnimation(0, first, first, last));
        enemy = GetComponent<Enemy>();
        monsterHpBar = GameObject.Find("Canvas/monsterSlider");
    }

    private void Update(){
        this.first = this.last;
        monsterHpBar.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 0.8f, transform.position.z));
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
