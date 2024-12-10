using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection.Emit;
[ExecuteAlways]

[RequireComponent(typeof(TextMeshPro))]
public class Cordinatelabeler : MonoBehaviour
{
    [SerializeField] Color DefaultColor = Color.white;
    [SerializeField] Color BlockedColor = Color.gray;
    [SerializeField] Color ExploredColor = Color.yellow;
    [SerializeField] Color PathColor = new Color (1f,0.5f,0f);
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        gridManager = FindObjectOfType<GridManager>();
        
        DisplayCoordinates();
        
    }
    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            updateObjectName();
            label.enabled=true;
        }

        SetLabelColor();
        ToogleLabels();
    
    }

    void SetLabelColor()
    {
        if(gridManager == null){return;}

        Node node = gridManager.GetNode(coordinates);

        if(node == null){return;}

        if(!node.isWalkable)
        {
            label.color = BlockedColor;
        }
          else if (node.isPath)
        {
            label.color = PathColor;
        }

        else if (node.isExplored)
        {
            label.color = ExploredColor;
        }
        else 
        {
            label.color = DefaultColor;
        }
    }

    void ToogleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }

    }

    void DisplayCoordinates()
    {
        if(gridManager == null ){return;}
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.unityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.unityGridSize);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void updateObjectName ()
    {
        transform.parent.name = coordinates.ToString();
    }
}
