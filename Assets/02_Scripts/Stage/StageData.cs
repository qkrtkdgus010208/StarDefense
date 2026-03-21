using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData")]
public class StageData : ScriptableObject
{
    public int stageNumber;        // 스테이지 번호
    public WaveInfo[] waves;   // 이 스테이지에 포함된 웨이브 리스트
}

[System.Serializable]
public class WaveInfo
{
    public GameObject enemyPrefab; // 소환할 몬스터 프리팹
    public int count;              // 이번 웨이브에 나올 몬스터 수
    public float spawnInterval;    // 몬스터 간 스폰 간격
    public float waitAfterWave;    // 이번 웨이브가 끝난 후 다음 웨이브까지 대기 시간
}
