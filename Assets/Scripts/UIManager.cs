using UnityEngine;
using System.Linq;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _enemyWithLowHP;

    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void UpdateLowHP()
    {
        if (GameManager.Instance.allGridEntitiesCurrentLife.Any())
            _enemyWithLowHP.text = "Lowest enemy HP is " + GameManager.Instance.allGridEntitiesCurrentLife.First();
    }
    public void UpdateEnemiesAlive(List<GridEntity> entities)
    {
        _text.text = "Percentage of enemies Alive " + UpdateUI(entities).ToString("F1");
    }
    float UpdateUI(List<GridEntity> entities)
    {
        var count = entities.Aggregate(0f, (x, y) =>   //IA2-P1 AGGREGATE
        {
            if (y.myFaction == Faction.ENEMY)
                return x + 1;
            else return x;
        });

        return (count / GameManager.Instance.totalGridEntities) * 100f;
    }
}
