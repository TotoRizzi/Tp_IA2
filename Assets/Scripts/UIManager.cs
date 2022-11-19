using UnityEngine;
using System.Linq;
using TMPro;
using System;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void UpdateEnemiesAlive(List<GridEntity> entities)
    {
        _text.text = "Percentage of enemies Alive " + UpdateUI(entities).ToString(); 
    }
    float UpdateUI(List<GridEntity> entities)
    {
        var count = entities.Aggregate(Tuple.Create(0f, 0f), (x, y) =>
        {
            if (y.myFaction == Faction.ENEMY)
                return Tuple.Create(x.Item1 + 1, x.Item2 + 1);
            else return Tuple.Create(x.Item1, x.Item2 + 1);
        });

        return (count.Item1 / count.Item2) * 100;
    }
}
