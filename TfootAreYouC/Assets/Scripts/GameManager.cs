using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currentChess;

    public Sprite currentChessSprite;

    public Transform tiles;

    public LayerMask tileMask;

    public int sands;

    public TextMeshProUGUI sandText;

    public LayerMask sandMask;

    public void BuyChess(GameObject chess, Sprite sprite)
    {
        currentChess = chess;
        currentChessSprite = sprite;
    }

    private void Update()
    {
            sandText.text = sands.ToString();

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);

            foreach(Transform tile in tiles)
                tile.GetComponent<SpriteRenderer>().enabled = false;

            if(hit.collider && currentChess)
            {
                hit.collider.GetComponent<SpriteRenderer>().sprite = currentChessSprite;
                hit.collider.GetComponent<SpriteRenderer>().enabled = true;
                
                if(Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().hasChess)
                {
                    Instantiate(currentChess, hit.collider.transform.position, Quaternion.identity);
                    hit.collider.GetComponent<Tile>().hasChess = true;
                    currentChess = null;
                    currentChessSprite = null;
                }
                
            }

            RaycastHit2D sandHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, sandMask);

            if (sandHit.collider)
            {
            if (Input.GetMouseButtonDown(0))
                {
                    sands += 25;
                    Destroy(sandHit.collider.gameObject);
                }
            }


    }



}
