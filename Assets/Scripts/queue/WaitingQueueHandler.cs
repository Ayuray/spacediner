using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WaitingQueueHandler : MonoBehaviour
{
    [Header("UI")]
    public RectTransform queueParent;     // Parent im Canvas (z.B. Panel)
    public Image alienPrefab;           // Prefab: UI Image mit RectTransform

    [Header("Random Sprites")]
    public List<Sprite> alienSprites = new List<Sprite>();

    [Header("Queue Layout")]
    public int queueSize = 3;
    public Vector2 rightmostPosition = new Vector2(440, 100);
    public float distanceRight = 80f;
    public float distanceUp = 0f;

    [Header("Spawn Timing")]
    public float spawnInterval = 20.0f;

    private float timer;
    private WaitingQueue queue;

    private void OnEnable()
    {
        DishSystem.OnOrderEnd += OnLeaveQueue;
    }

    private void OnDisable()
    {
        DishSystem.OnOrderEnd -= OnLeaveQueue;
    }

    private void Start()
    {
        // create slots from right to left
        List<Vector2> slotPositions = new List<Vector2>();
        for (int i = 0; i < queueSize; i++)
        {
            slotPositions.Add(rightmostPosition + (Vector2.left * i * distanceRight) + (Vector2.up * i * distanceUp));
        }

        queue = new WaitingQueue(slotPositions);
    }

    private void OnEnterQueue()
    {
        TrySpawnAlien();
    }

    private void OnLeaveQueue()
    {
        queue.TryRemoveQueue();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            TrySpawnAlien();
            timer += spawnInterval;
        }
    }

    private void TrySpawnAlien()
    {
        if (queue == null || queueParent == null || alienPrefab == null) return;
        if (queue.queueFull) return;
        if (alienSprites == null || alienSprites.Count == 0) return;

        // choose random Sprite
        Sprite sprite = alienSprites[UnityEngine.Random.Range(0, alienSprites.Count)];

        // create Alien Image
        Image img = Instantiate(alienPrefab, queueParent);
        img.sprite = sprite;
        img.transform.SetAsFirstSibling();

        RectTransform rt = img.GetComponent<RectTransform>();

        // start outside on the left
        rt.anchoredPosition = rightmostPosition + (Vector2.left * queueSize * distanceRight) - new Vector2(200f, 0f);

        // enter queue (sets position automatically)
        queue.TryAddQueue(img.gameObject);
    }
}