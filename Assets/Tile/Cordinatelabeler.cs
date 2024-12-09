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
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint =GetComponentInParent<Waypoint>();
        
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
        if (waypoint.isPlaceable)
        {
            label.color = DefaultColor;
        }
        else
        {
            label.color = BlockedColor;
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
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);

        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void updateObjectName ()
    {
        transform.parent.name = coordinates.ToString();
    }
}
