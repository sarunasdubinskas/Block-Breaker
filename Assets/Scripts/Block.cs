using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparkles;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;
    
    //cached refs
    Level level;
    GameStatus gameStatus;

    //state vars
    [SerializeField] int timesHit; //debug info

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameStatus>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.Log("Block sprite is missing from array" + gameObject.name);
        }
    }

    public void DestroyBlock()
    {
        PlayDestroySound();
        gameStatus.AddToScore();
        Destroy(gameObject);
        level.CountBlocksLeft();
        TrigerSparklesVFX();
    }

    private void PlayDestroySound()
    {
        Vector3 blockPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        AudioSource.PlayClipAtPoint(breakSound, blockPos);
    }

    private void TrigerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparkles, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

}
