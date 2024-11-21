using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class Gauge : MonoBehaviour
{
    public Image gauge;
    //public Text gaugeText;
    private Item item;
    private float first = 0f; //경험치 처음 구간
    private float last = 0f; //경험치가 들어간 마지막 구간
    private float elapsedTime = 0f;
    private float currentValue = 0f;
    private float EXP; //Enemy에서 나오는 경험치 or 상자 같은 것으로 획득하는 경우
    private Enemy enemy;
    private GameObject EXPbar;
    //private Rigidbody2D rb;

    private void Start()
    {
        StartCoroutine(GaugeAnimation(0, 100, first, last, 3f));
        enemy = FindObjectOfType<Enemy>();
        EXPbar = GameObject.Find("Canvas/EXPbar");
        //rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        this.first = this.last;
        this.last = other_EXP();
    }

    public float other_EXP(){
        EXP = enemy.Enemy_EXP;
        return EXP;
    }

//https://notyu.tistory.com/62
    private IEnumerator GaugeAnimation(float min, float max, float f, float l, float animationTime)
    {
        
        
        while(elapsedTime < animationTime)
        {
            currentValue = Mathf.Lerp(f, l, animationTime);
            //예를 들어, Mathf.Lerp(0, 10, 0.5f)는 0과 10 사이에서 중간 값인 5를 반환합니다


            gauge.fillAmount = (currentValue - min) / (max - min);
            //gaugeText.text = currentValue.ToString();
            //toString은 그냥 text에 입력하는 것

            //elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}