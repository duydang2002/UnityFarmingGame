using DateTimeNameSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public Crop crop;
    public int growTimer;
    public SpriteRenderer renderer;
    public int growStage;
    public float damage;
    public Vector3Int position;

    public bool Complete
    {
        get
        {
            if (crop == null) { return false; }
            return growTimer >= crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growTimer = 0;
        growStage = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
        damage = 0;
    }
}

public class CropsManager : TimeManagerScript
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] GameObject cropSpritePrefab;
    [SerializeField] LevelManager levelManager;


    Dictionary<Vector2Int, CropTile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropTile>();
        OnDateTimeChanged?.Invoke(dateTime);
    }
    private void Update()
    {
        currentTimeBetweenTicks += Time.deltaTime;
        if (currentTimeBetweenTicks >= timeBetweenTicks)
        {
            currentTimeBetweenTicks = 0;
            foreach (CropTile cropTile in crops.Values)
            {
                if (cropTile.crop == null)
                {
                    continue;
                }
                cropTile.damage += 0.02f;

                if (cropTile.damage > 1f)
                {
                    cropTile.Harvested();
                    targetTilemap.SetTile(cropTile.position, plowed);
                    continue;
                }

                if (cropTile.Complete)
                {
                    Debug.Log("Done growth");
                    continue;
                }

                cropTile.growTimer += 1;

                if(cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
                {
                    cropTile.renderer.gameObject.SetActive(true);
                    cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];

                    cropTile.growStage += 1;
                }


            }
        }
    }
    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }
    
    public void Plow(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
        {
            return;
        }

        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        targetTilemap.SetTile(position, seeded);

        crops[(Vector2Int)position].crop = toSeed;
    }
    
    
    private void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop);

        GameObject go = Instantiate(cropSpritePrefab);
        go.transform.position = targetTilemap.CellToWorld(position);
        go.SetActive(false);
        crop.renderer = go.GetComponent<SpriteRenderer>();

        crop.position = position;

        targetTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int postion = (Vector2Int)gridPosition;
        if(crops.ContainsKey(postion)== false)
        {
            return;
        }
        CropTile cropTile = crops[postion];
        
        if (cropTile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(
                targetTilemap.CellToWorld(gridPosition), 
                cropTile.crop.yield, 
                cropTile.crop.count);
            levelManager.AddExp(10);
            targetTilemap.SetTile(gridPosition, plowed);
            cropTile.Harvested();
        }
    }

}