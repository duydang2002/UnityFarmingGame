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
}

public class CropsManager : TimeManagerScript
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] GameObject cropSpritePrefab;


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
                cropTile.growTimer += 1;

                if(cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
                {
                    cropTile.renderer.gameObject.SetActive(true);
                    cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];

                    cropTile.growStage += 1;
                }

                if (cropTile.growTimer >= cropTile.crop.timeToGrow)
                {
                    Debug.Log("Done growth");
                    cropTile.crop = null;
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

        targetTilemap.SetTile(position, plowed);
    }
}