using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameStartUI : MonoBehaviour, IPointerClickHandler{
    private MapEnter mapEnter;
    // Start is called before the first frame update

    [SerializeField]
    public Button startUI;
    [SerializeField]
    public Button endUI;

    private void Start()
    {
        mapEnter = FindObjectOfType<MapEnter>();
    }
    //mapEnter.Enter;
    

    // Update is called once per frame
    private void Update()
    {
        // if(true){
            
        // }
    }

    private void OnCollisionEnter2D(Collision mouse) {
        if(mouse.gameObject.tag == "startUI"){

            PointerEventData eventData = new PointerEventData(EventSystem.current){
                button = PointerEventData.InputButton.Left
            }; //class
            OnPointerClick(eventData);
        }

        else if(mouse.gameObject.tag == "endUI"){
            PointerEventData eventData = new PointerEventData(EventSystem.current){
                button = PointerEventData.InputButton.Left
            }; //class
            OnPointerClick(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        if (eventData.button == PointerEventData.InputButton.Left){
            mapEnter.MapChange();
            Debug.Log("is clicked by mouse left button click");
            //to move next scene
        }
    }
}
