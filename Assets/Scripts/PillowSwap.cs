using UnityEngine;
using System.Collections.Generic;

public class PillowSwap : MonoBehaviour
{
    
    public GameObject safeAreaPrefab; // Reference to SafeArea prefab
    private List<Vector3> prefabPositions = new List<Vector3>();
    public TextMesh textObject; // Reference to the TextMesh component

    public ConsoleToText consoleToText; // Reference to the ConsoleToText script

    public List<Vector3> GetPrefabPositions()
    {
        return prefabPositions;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            Vector3 objectPosition = transform.position;
            objectPosition.y = 0.1f;

            GameObject newSafeArea = Instantiate(safeAreaPrefab, objectPosition, Quaternion.identity);
            Vector3 prefabPosition = newSafeArea.transform.position;
            prefabPositions.Add(prefabPosition);

            // Pass the prefabPositions list instead of a single Vector3
            consoleToText.PrintPrefabPositions(prefabPositions);

            Destroy(gameObject);
        }
    }

    
     public void PrintPositions(TextMesh textObject)
        {
            Debug.Log("Text object positions:");
            for (int i = 0; i < textObject.transform.position.magnitude; i++)
            {
                Debug.Log(textObject.transform.position[i].ToString());
            }
        }
    }

