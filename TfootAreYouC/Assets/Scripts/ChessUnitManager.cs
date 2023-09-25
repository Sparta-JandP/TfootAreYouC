using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessUnitManager : MonoBehaviour
{
    public GameObject unitPrefab;   // 유닛 프리팹
    private List<ChessUnit> chessUnits = new List<ChessUnit>(); // 유닛 목록

    
    // 유닛 생성
    public ChessUnit CreateUnit(string name, Vector3 position)
    {
        GameObject unitObject = Instantiate(unitPrefab, position, Quaternion.identity);
        ChessUnit unit = unitObject.GetComponent<ChessUnit>();
        unit.Initialize(name, 100, 10, 2.0f);   //초기값 설정
        chessUnits.Add(unit);
        return unit;
    }

    // 유닛 삭제
    public void DestroyUnit(ChessUnit unit)
    {
        chessUnits.Remove(unit);
        Destroy(unit.gameObject);
    }
    

}
