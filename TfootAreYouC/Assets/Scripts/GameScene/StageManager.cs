using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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

    public List<GameObject> StageObjects;

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
    private void OnEnable()
    {
        UnitController.OnDestroyed += RemoveObjectFromList;
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
            curKing.GetComponent<HealthSystem>().ChangeHealth(-maxKingHealth);
            OnDefeat(); //패배 출력
        }
        OnKingHealthChange?.Invoke();
    }

    public void EnemyBossHealth() //적 보스의 체력
    {
        if(bossHealth <= 0)
        {
            curBoss.GetComponent<HealthSystem>().ChangeHealth(-maxBossHealth);
            Debug.Log(curBoss.GetComponent<HealthSystem>());
            if (currentStage < 4)
            {
                StageClear();
            }
            else if(currentStage >= 4)
            {
                OnVictory(); //승리 출력
            }
        }
        OnBossHealthChange?.Invoke();
    }

    public void OnDefeat()
    {
        StartCoroutine(DefeatPause());
        Time.timeScale = 0f;
        foreach (GameObject obj in StageObjects)
        {
            Destroy(obj);
        }
        StageObjects.Clear();
    }

    public void OnVictory()
    {
        StartCoroutine(WinPause());
        Time.timeScale = 0f;
        foreach (GameObject obj in StageObjects)
        {
            Destroy(obj);
        }
        StageObjects.Clear();
    }

    public void OnPause() //일시정지 
    {
        Time.timeScale = 0f;
    }

    public void OnResume() //계속하기 버튼
    {
        Time.timeScale = 1f;
    }

    public void OnRestart() //처음부터 버튼
    {
        SoundManager.instance.PlayEffect("positive");
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(2);
    }

    public void OnHome() //홈으로 가는 버튼
    {
        SoundManager.instance.PlayEffect("negative");
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    void StageClear()
    {
        StartCoroutine(StageClearPause());
        Time.timeScale = 0f;
        foreach (GameObject obj in StageObjects)
        {
            Destroy(obj);
        }
        StageObjects.Clear();
    }

    IEnumerator StageClearPause() 
    {
        currentStage++;
        Debug.Log(OnStageClear);
        OnStageClear?.Invoke();
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(curBoss);
        yield return new WaitForSecondsRealtime(3f);
        Destroy(curKing);
        SetKingBoss();
        OnStageResume?.Invoke();
        Time.timeScale = 1f;
    }

    IEnumerator WinPause()
    {
        OnWin?.Invoke();
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(curBoss);
        yield return new WaitForSecondsRealtime(3f);
        Destroy(curKing);
    }

    IEnumerator DefeatPause()
    {
        OnGameOver?.Invoke();
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(curKing);
        yield return new WaitForSecondsRealtime(3f);
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

    private void RemoveObjectFromList(GameObject obj)
    {
        if (StageObjects == null) return;

        if (StageObjects.Contains(obj))
        {
            StageObjects.Remove(obj);
        }
    }

    private void OnDisable()
    {
        UnitController.OnDestroyed -= RemoveObjectFromList;
    }

    public void Win()
    {
        bossHealth = 10;
        DamageBoss(10);
    }
}