using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
public class GameManager : MonoBehaviour
{
    public Player player;

    public static GameManager Instance;

    public List<GridEntity> allGridEntities = new List<GridEntity>();

    public List<string> allNames = new List<string>();

    public List<string> allGridEntitiesCurrentLife = new List<string>();

    public int totalGridEntities;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        foreach (var item in SetNames()) { }
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
        if (allGridEntities.Where(x => x.myFaction == Faction.ENEMY).Count() <= 0)
            SceneManager.LoadScene(1);
    }
    public void UpdateGridEntityCurrentLife()
    {
        allGridEntitiesCurrentLife = allGridEntities.Where(x => x.myFaction == Faction.ENEMY).OrderBy(x => x.CurrentLife).Select(x => x.myName).ToList();
        UIManager.Instance.UpdateLowHP();
    }
    IEnumerable<Tuple<GridEntity, string>> SetNames()
    {
        return allGridEntities.Zip(allNames, (x, y) =>
        {
            x.myName = y;
            return Tuple.Create(x, x.myName);
        });
    }
}
