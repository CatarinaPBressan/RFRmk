using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VehicleBase : MonoBehaviour {

    private Image FuelBar;
    private Image HealthBar;
    private Image MunitionBar;
    private HealthCounter HealthCnt;
    private ShootingBehaviour ShootingBhv;

    protected float CurrentFuelUnits;
    public int MaxFuelUnits = 100;

    void Start()
    {
        CurrentFuelUnits = MaxFuelUnits;


        FuelBar = transform.Find("Stats/FuelBar").GetComponent<Image>();  
        HealthBar = transform.Find("Stats/LifeBar").GetComponent<Image>();
        MunitionBar = transform.Find("Stats/MunitionBar").GetComponent<Image>();

        HealthCnt = GetComponent<HealthCounter>();
        ShootingBhv = GetComponent<ShootingBehaviour>();


    }


    protected void FixedUpdate()
    {

        UpdateBar();
    }


    void UpdateBar()
    {

        FuelBar.fillAmount = CurrentFuelUnits / 100;
        HealthBar.fillAmount = HealthCnt.CurrentHealth / HealthCnt.InitialHealth;
        MunitionBar.fillAmount = (float)ShootingBhv.CurrentAmmoCount / (float)ShootingBhv.MaxAmmoSize;


    }

}
