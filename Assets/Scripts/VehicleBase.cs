using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleBase : MonoBehaviour {

    private Image FuelBar;
    protected float CurrentFuelUnits;
    public int MaxFuelUnits = 100;

    void Start()
    {
        CurrentFuelUnits = MaxFuelUnits;


        if (transform.Find("FuelNivel/FuelBar") != null)
        {
        
            FuelBar = transform.Find("FuelNivel/FuelBar").GetComponent<Image>();
        }
    }


    protected void FixedUpdate()
    {

        UpdateBar();
    }


    void UpdateBar()
    {

        FuelBar.fillAmount = CurrentFuelUnits / 100;

    }

}
