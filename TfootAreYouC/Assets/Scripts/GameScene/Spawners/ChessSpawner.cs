using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ChessSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Chesses = new GameObject[6];
    [SerializeField] Sprite[] ChessSprites = new Sprite[6];
    [SerializeField] SpriteRenderer _currentChessSprite;
    [SerializeField] LayerMask _chessLayer;
    
    private GameObject _currentChess;
    private int _chessPrice;
    StageManager _stageManager;


    //추가 부분
    [SerializeField] private Tilemap tilemap;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        _stageManager = StageManager.instance;
        _currentChessSprite.enabled = false;
    }

    public void SelectChess(int buttonValue)
    {
        _currentChess = Chesses[buttonValue];
        _currentChessSprite.sprite = ChessSprites[buttonValue];
    }

    public void Pricing(int price)
    {
        _chessPrice = price;
    }

    private void Update()
    {
        if (_chessPrice <= _stageManager.mineral)
        {
            Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            Vector3Int coordinate = tilemap.WorldToCell(mouseWorldPos);

            if (tilemap.HasTile(coordinate) && (hit.collider == null || (_chessLayer.value != (_chessLayer.value | (1 << hit.collider.gameObject.layer)))))
            {
                if (_currentChess != null)
                {
                    _currentChessSprite.enabled = true;
                    _currentChessSprite.color = new Color32(255, 255, 255, 100);
                    _currentChessSprite.transform.position = tilemap.GetCellCenterWorld(coordinate);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_stageManager.BuyingChess(_chessPrice))
                        { 
                            GameObject obj = Instantiate(_currentChess, tilemap.GetCellCenterWorld(coordinate), Quaternion.identity);
                            _stageManager.StageObjects.Add(obj);
                        }
                        ResetSelection();
                    }
                }
            }
            else
            {
                _currentChessSprite.enabled = false;
            }
        }
        else
        {
            ResetSelection();
        }
    }

    private void ResetSelection()
    {
        _currentChess = null;
        _currentChessSprite.color = new Color32(255, 255, 255, 255);
        _currentChessSprite.sprite = null;
        _currentChessSprite.enabled = false;
        _chessPrice = 0;
    }

}
