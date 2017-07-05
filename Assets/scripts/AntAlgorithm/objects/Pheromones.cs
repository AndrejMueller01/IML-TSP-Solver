﻿/****************************************************
 * IML ACO implementation for TSP 
 * More information: http://hci-kdd.org/project/iml/
 * Author: Andrej Mueller
 * Year: 2017
 *****************************************************/

/* Pheromone represents the pheromones between cities */

using System;

public class Pheromones
{
    // initialization factor for pheromones
    private double initPheromoneValue;
    // Matrix of pheromones between city x and city y
    private double[][] pheromones;
    private int numOfCities;
    // for mmas
    private double trailMin;
    private double trailMax;

    public Pheromones(int numOfCities, double initPheromoneValue)
    {
        this.numOfCities = numOfCities;
        this.initPheromoneValue = initPheromoneValue;
    }

    public Pheromones(int numOfCities, double initPheromoneValue, double pBest)
    {
        this.numOfCities = numOfCities;
        this.initPheromoneValue = initPheromoneValue;

        trailMax = initPheromoneValue;
        trailMin = (trailMax * (1.0 - Math.Pow(pBest, numOfCities))) / (((numOfCities / 2) - 1.0) * Math.Pow(pBest, numOfCities));
    }

    // init of pheromones
    public void Init()
    {
        pheromones = new double[numOfCities][];
        for (int i = 0; i < numOfCities; i++)
            pheromones[i] = new double[numOfCities];
        for (int i = 0; i < pheromones.Length; i++)
            for (int j = 0; j < pheromones[i].Length; j++)
                pheromones[i][j] = initPheromoneValue;
    }

    public new string ToString
    {
        get
        {
            string str = "";
            for (int i = 0; i < pheromones.Length; i++)
            {
                str += "\n";
                for (int j = 0; j < pheromones[i].Length; j++)
                    str += pheromones[i][j] + " ";
            }
            return str;
        }
    }
    
    public void CheckPheromoneTrailLimits()
    {
        for (int i = 0; i < numOfCities; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (pheromones[i][j] < trailMin)
                {
                    pheromones[i][j] = trailMin;
                    pheromones[j][i] = trailMin;
                }
                else if (pheromones[i][j] > trailMax)
                {
                    pheromones[i][j] = trailMax;
                    pheromones[j][i] = trailMax;
                }
            }
        }
    }

    public void UpdateTrailLimits(double optimalLength, double rho, double pBest)
    {
        trailMax = 1.0/ (rho * optimalLength);
        trailMin = (trailMax * (1.0 - Math.Pow(pBest, numOfCities))) / (((numOfCities / 2) - 1.0) * Math.Pow(pBest, numOfCities));
    }
    // decrease the pheromone value between 2 particular cities by one ant 
    public void DecreasePheromoneAs(int cityAId, int cityBId, double decreaseValue)
    {
        pheromones[cityAId][cityBId] = decreaseValue * pheromones[cityAId][cityBId];
    }

    // decrease the pheromone value between 2 particular cities by one ant 
    public void IncreasePheromoneAs(int cityAId, int cityBId, double increaseValue)
    {
        pheromones[cityAId][cityBId] = pheromones[cityAId][cityBId] + increaseValue;
    }

    public void SetPheromone(int cityAId, int cityBId, double value)
    {
        pheromones[cityAId][cityBId] = value;
    }

    public double GetPheromone(int cityAId, int cityBId)
    {
        return pheromones[cityAId][cityBId];
    }
}
