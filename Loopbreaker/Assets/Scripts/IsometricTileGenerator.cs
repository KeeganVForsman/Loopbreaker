using UnityEngine;

public class IsometricTileGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    public float tileWidth = 1f;
    public float tileHeight = 0.5f;

    [Header("Tile Prefab")]
    public GameObject tilePrefab;

    void Start()
    {
        GenerateIsometricGrid();
    }

    void GenerateIsometricGrid()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("Tile Prefab is not assigned!");
            return;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 isoPos = GridToIsometric(x, y);
                GameObject tile = Instantiate(tilePrefab, new Vector3(isoPos.x, 0, isoPos.y), Quaternion.identity);
                tile.name = $"Tile_{x}_{y}";
                tile.transform.SetParent(transform);

                // Optional: handle sorting if using 2D sprites
                SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sortingOrder = -(x + y);
                }
            }
        }
    }

    Vector2 GridToIsometric(int x, int y)
    {
        float isoX = (x - y) * tileWidth / 2f;
        float isoY = (x + y) * tileHeight / 2f;
        return new Vector2(isoX, isoY);
    }
}