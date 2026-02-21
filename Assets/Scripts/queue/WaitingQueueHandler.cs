using System.Collections.Generic;
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
    public float distance = 80f;

    [Header("Spawn Timing")]
    public float spawnInterval = 1.0f;

    private float timer;
    private WaitingQueue queue;

    private void Start()
    {
        // Slots von links nach rechts erzeugen
        List<Vector2> slotPositions = new List<Vector2>();
        for (int i = 0; i < queueSize; i++)
        {
            slotPositions.Add(rightmostPosition + Vector2.left * i * distance);
        }

        queue = new WaitingQueue(slotPositions);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            TrySpawnPerson();
            timer = spawnInterval;
        }
    }

    private void TrySpawnPerson()
    {
        if (queue == null || queueParent == null || alienPrefab == null) return;
        if (queue.queueFull) return;
        if (alienSprites == null || alienSprites.Count == 0) return;

        // Zufälligen Sprite wählen
        Sprite sprite = alienSprites[Random.Range(0, alienSprites.Count)];

        // Person-Image erzeugen
        Image img = Instantiate(alienPrefab, queueParent);
        img.sprite = sprite;

        // optional: native size, falls ihr das wollt:
        // img.SetNativeSize();

        // In Queue einreihen (setzt automatisch die Position)
        queue.tryEnqueue(img.gameObject);
    }
}