using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;

    public static GameManager Instance;

    public List<GridEntity> allGridEntities = new List<GridEntity>();
    private void Awake()
    {
        Instance = this;
    }
    public void AddGridEntity(GridEntity g)
    {
        allGridEntities.Add(g);

        UIManager.Instance.UpdateEnemiesAlive(allGridEntities);
    }
    public void RemoveGridEntity(GridEntity g)
    {
        allGridEntities.Remove(g);

        UIManager.Instance.UpdateEnemiesAlive(allGridEntities);
    }
}
