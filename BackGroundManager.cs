/*
using System.Collections;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject blockPrefab; // 블록 프리팹
    [SerializeField]
    public float spawnInterval; // 블록이 생성되거나 사라지는 간격

    private void Start()
    {
        StartCoroutine(SpawnBlocks());
    }

    IEnumerator SpawnBlocks()
    {
        while (true)
        {
            // 새 블록 생성, 랜덤성을 넣자
            //그냥 일반 블럭, 녹는 블럭, 불타는 블럭, 터지는 블럭, 랜덤 요소
            //난이도가 높아질 수록 확률은 변동
            GameObject newBlock = Instantiate(blockPrefab, new Vector3(0, 5, 0), Quaternion.identity);
            newBlock.transform.SetParent(transform); // 블록이 배경의 자식으로 추가되게 설정

            // 일정 시간 대기
            yield return new WaitForSeconds(spawnInterval);

            // 블록 제거
            Destroy(newBlock);
        }
    }
}
*/
