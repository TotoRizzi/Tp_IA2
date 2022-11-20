using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameManager : MonoBehaviour
{
    public Player player;

    public static GameManager Instance;

    public List<GridEntity> allGridEntities = new List<GridEntity>();

    public int totalGridEntities;
    private void Awake()
    {
        Instance = this;
    }
    public void AddGridEntity(GridEntity g)
    {
        allGridEntities.Add(g);
        if (g.myFaction == Faction.ENEMY) totalGridEntities++;
        UIManager.Instance.UpdateEnemiesAlive(allGridEntities);
    }
    public void RemoveGridEntity(GridEntity g)
    {
        allGridEntities.Remove(g);
        UIManager.Instance.UpdateEnemiesAlive(allGridEntities);
    }
}
