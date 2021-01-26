using UnityEngine;

[CreateAssetMenu(fileName = "plant_00", menuName = "ScriptableObjects/Plant")]
public class Plant : ScriptableObject
{
 
    public int id = 0, cost = 100;
    public GameObject prefab;

}
