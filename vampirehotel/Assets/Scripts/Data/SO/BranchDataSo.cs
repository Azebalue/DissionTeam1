using UnityEngine;

[CreateAssetMenu(fileName = "BranchSO", menuName = "GameData/Branch")]
public class BranchDataSo : ScriptableObject
{
    [SerializeField]
    private int id;
    public int ID { get { return id; } }    


}
