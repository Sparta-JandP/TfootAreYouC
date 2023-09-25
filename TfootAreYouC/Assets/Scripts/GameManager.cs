using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currentChess;

    public Sprite currentChessSprite;

    public Transform tiles;

    public LayerMask tileMask;

    public void BuyChess(GameObject chess, Sprite sprite)
    {
        currentChess = chess;
        currentChessSprite = sprite;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);

            foreach(Transform tile in tiles)
                tile.GetComponent<SpriteRenderer>().enabled = false;

            if(hit.collider && currentChess)
        {
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentChessSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    

    
}
