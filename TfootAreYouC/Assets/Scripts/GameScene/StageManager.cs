using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [SerializeField] private LayerMask _sandMask;
    
    [SerializeField] private Tilemap board;
    public int start;
    public int end;
    public Vector3 startXPos;
    public Vector3 endXPos;

    public int mineral; //현재 자원
    public int mine; //일꾼 유닛이 채굴한 자원
    public int maxMineral; //최대 자원
    public int kingHealth; //왕의 체력
    public int bossHealth; //적 보스의 체력
    public int maxKingHealth; //최대 왕의 체력
    public int maxBossHealth; //최대 보스 체력
    public int currentStage; //현재 스테이지

    public event Action OnStageClear;
    public event Action OnStageResume;
    public event Action OnWin;
    public event Action OnGameOver;
    public event Action OnKingHealthChange;
    public event Action OnBossHealthChange;
    public event Action OnSandAmountChange;

    public GameObject King;
    private GameObject curKing;
    private GameObject curBoss;
    public GameObject[] bossCards;

    private void Awake()
    {
        // 이미 인스턴스가 있는지 확인하고, 없으면 현재 스크립트를 인스턴스로 설정
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
            return;
        }


        currentStage = 1;
        SetKingBoss();
    }

    private void Start()
    {
        start = board.cellBounds.min.x;
        end = board.cellBounds.max.x - 1;

        Vector3Int startPosition = new Vector3Int(start, 0, 0);
        startXPos = board.GetCellCenterWorld(startPosition);

        Vector3Int endPosition = new Vector3Int(end, 0, 0);
        endXPos = board.GetCellCenterWorld(endPosition);

        kingHealth = maxKingHealth;
        bossHealth = maxBossHealth;
    }

    private void Update()
    {
        RaycastHit2D sandHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, _sandMask);

        if (sandHit.collider)
        {
        if (Input.GetMouseButtonDown(0))
            {
                Mining();
                Destroy(sandHit.collider.gameObject);
            }
        }
    }

    private void Mining() //채굴 유닛의 버튼 클릭 시
    {
        mineral += mine; //채굴량 설정값 만큼 자원 추가
        maxMineral = 400; //최대 자원값
        mineral = Mathf.Clamp(mineral, 0, maxMineral); //자원 최대 최소값 설정

        OnSandAmountChange?.Invoke();
    }

    public bool BuyingChess(int price)
    {
        if (mineral - price >= 0)
        {
            mineral -= price;
            OnSandAmountChange?.Invoke();
            return true;
        }
        else return false;
    }
    

    public void AllyKingHealth() //내 왕의 체력
    {
        if(kingHealth <= 0)
        {
            OnDefeat(); //패배 출력
        }
        OnKingHealthChange?.Invoke();
        Debug.Log($"킹: {kingHealth}");
    }

    public void EnemyBossHealth() //적 보스의 체력
    {
        if(bossHealth <= 0)
        {
            OnVictory(); //승리 출력
        }
        OnBossHealthChange?.Invoke();
        Debug.Log($"보스: {bossHealth}");
    }

    public void OnDefeat()
    {
        //패배 결과 출력
        OnGameOver?.Invoke();
        Time.timeScale = 1f;
    }

    public void OnVictory()
    {
        //승리 결과 출력
        OnWin?.Invoke();
        Time.timeScale = 1f;
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
    }

    public void OnResume()
    {
        Time.timeScale = 1f;
    }

    public void OnResetart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(2);
    }

    public void OnHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    void StageClear()
    {
        StartCoroutine(StageClearPause());
        OnStageClear?.Invoke();
        Time.timeScale = 0f;
        //스테이지 정보 업데이트 및 오브젝트 파괴 등 새 스테이지 세팅
        SetKingBoss();
    }

    IEnumerator StageClearPause() 
    {
        yield return new WaitForSecondsRealtime(3f);
        OnStageResume?.Invoke();
        Time.timeScale = 1f;
    }

    void SetKingBoss()
    {
        if (currentStage >= 1 && currentStage <= 4)
        {
            // 현재 스테이지에 해당하는 보스 카드를 인스턴스화합니다.
            curBoss = Instantiate(bossCards[currentStage - 1]);
            curBoss.transform.position = new Vector3(7f, 0.7f, 0f);

            // 킹 게임 오브젝트를 인스턴스화합니다.
            curKing = Instantiate(King);
            curKing.transform.position = new Vector3(-7f, 0.7f, 0f);
        }

        maxKingHealth = curKing.GetComponent<UnitController>().maxHealth; //왕의 최대 체력
        maxBossHealth = curBoss.GetComponent<UnitController>().maxHealth; //보스의 최대 체력

        kingHealth = maxKingHealth;
        bossHealth = maxBossHealth;
    }

    public void DamageBoss(int damage)
    {
        bossHealth -= damage;
        EnemyBossHealth();
    }

    public void DamageKing(int damage)
    {
        kingHealth -= damage;
        AllyKingHealth();
    }
}