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
        _text.text = "Percentage of enemies Alive " + UpdateUI(entities).ToString("F1");
    }
    float UpdateUI(List<GridEntity> entities)
    {
        var count = entities.Aggregate(0f, (x, y) =>
        {
            if (y.myFaction == Faction.ENEMY)
                return x + 1;
            else return x;
        });

        return (count / GameManager.Instance.totalGridEntities) * 100f;
    }
}
