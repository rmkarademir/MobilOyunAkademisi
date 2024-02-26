using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class City : MonoBehaviour
{
    public int  money,
                day,
                curPopulation,
                curJobs,
                curFood,
                maxPopulation,
                maxJobs,
                incomePerJob;
    public TextMeshProUGUI statsText;
    public List<Building> buildings = new List<Building>();
    public static City instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        UpdateStatText();
    }

    void Update()
    {
        
    }
    public void OnPlaceBuilding(Building building)
    {
        money -= building.preset.cost;
        curPopulation += building.preset.population;
        curJobs += building.preset.jobs;
        buildings.Add(building);
        UpdateStatText();
    }
    public void OnRemoveBuilding(Building building)
    {
        money += (int)(building.preset.cost * 0.4f);
        curPopulation -= building.preset.population;
        curJobs -= building.preset.jobs;
        buildings.Remove(building);
        Destroy(building.gameObject);
        UpdateStatText();
    }
    void UpdateStatText()
    {
        statsText.text = $"Day: {day} Money: {money} Pop: {curPopulation}/{maxPopulation} Job: {curJobs}/{maxJobs} Food: {curFood} ";
    }
    public void EndDay()
    {
        day++;
        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        UpdateStatText();
    }

    private void CalculateFood()
    {
        curFood = 0;
        foreach (Building building in buildings)
        {
            curFood += building.preset.food;
        }
    }

    private void CalculateJobs()
    {
        curJobs = Mathf.Min(curPopulation, maxJobs);
    }

    private void CalculatePopulation()
    {
        if(curFood >= curPopulation && curPopulation < maxPopulation)
        {
            curFood -= curPopulation / 4;
            curPopulation = Mathf.Min(curPopulation + (curFood / 4), maxPopulation);
        }
        else if(curFood < curPopulation)
        {
            curPopulation = curFood;
        }
    }

    private void CalculateMoney()
    {
        money += curJobs * incomePerJob;
        foreach (Building building in buildings)
        {
            money -= building.preset.costPerDay;
        }
    }
}
