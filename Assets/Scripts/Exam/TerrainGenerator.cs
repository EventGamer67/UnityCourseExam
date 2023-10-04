using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _terrainBlocks;
    [SerializeField] private GameObject _terrainBorder;
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _grass;

    private int _terrainHeight = 20;
    private int _terrainWidth = 20;

    // Start is called before the first frame update
    void Start()
    {
        for(int x = -_terrainWidth/2; x < _terrainWidth; x++)
        {
            for (int z = -_terrainHeight/2; z < _terrainWidth; z++)
            {
                GameObject _terrainBlock = _terrainBlocks[Random.Range(0, _terrainBlocks.Count)];
                float posY = Random.Range(0, 0.2f);
                Instantiate(_terrainBlock, new Vector3(x, posY ,z), Quaternion.identity);
                if (Random.value < 0.2)
                {
                    Instantiate(_grass, new Vector3(x-2, posY+1.3f, z-2), Quaternion.identity);
                }
            }
        }
        Instantiate(_coin, new Vector3(Mathf.Floor(Random.Range(-_terrainWidth/2,_terrainWidth)), 2 , Mathf.Floor(Random.Range(-_terrainHeight / 2, _terrainHeight))), Quaternion.identity);

        for (int x = (-_terrainWidth / 2)-1; x < _terrainWidth+1; x++)
        {
            for(int y = 0; y < 10; y++)
            {
                Instantiate(_terrainBorder, new Vector3(x, y, (float)_terrainWidth), Quaternion.identity);
            }
        }
        for (int x = (-_terrainWidth / 2) - 1; x < _terrainWidth + 1; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Instantiate(_terrainBorder, new Vector3(x, y, -((float)_terrainWidth/2)-1 ), Quaternion.identity);
            }
        }
        for (int x = (-_terrainWidth / 2); x < _terrainWidth; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Instantiate(_terrainBorder, new Vector3( -((float)_terrainWidth / 2) - 1, y, x), Quaternion.identity);
            }
        }
        for (int x = (-_terrainWidth / 2); x < _terrainWidth; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Instantiate(_terrainBorder, new Vector3(_terrainWidth, y, x), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
