using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOccupant : MonoBehaviour
{
    [field: SerializeReference]
    public SpaceOccupant OccupantType { get; set; }
    
    public void Remove(int x, int y) {
        LevelCreator.GetInstance().GetCurrentGridManager().RemoveOccupant(x, y, this);
    }
}
