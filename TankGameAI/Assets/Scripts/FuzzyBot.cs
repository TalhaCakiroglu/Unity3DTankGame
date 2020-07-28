using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AForge.Fuzzy;
using System;
using AForge;

public class FuzzyBot : MonoBehaviour
{
    public Transform Player;
    float distance, speed;
    /*
    [Header("Distance fuzzy sets")]
    public AnimationCurve acNear;
    public AnimationCurve acMedi;
    public AnimationCurve acFar;

    [Header("Speed fuzzy sets")]
    public AnimationCurve acSlow;
    public AnimationCurve acMedium;
    public AnimationCurve acFast; //fuzzyde örnek kullanımı acFast.keys[0].time gibi
    */

    //mesafe
    FuzzySet fsNear, fsMedi, fsFar; //fs-> fuzzy küme
    LinguisticVariable lvDistance;


    //hız
    FuzzySet fsSlow, fsMedium, fsFast;
    LinguisticVariable lvSpeed;

    Database database;
    InferenceSystem infSystem;



    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        SetDistanceFuzzySets();
        SetSpeedFuzzySets();
        AddToDatabase();
    }

    private void SetDistanceFuzzySets()
    {
        fsNear = new FuzzySet("Near", new TrapezoidalFunction(20, 40, TrapezoidalFunction.EdgeType.Right));
        fsMedi = new FuzzySet("Medi", new TrapezoidalFunction(20, 40, 50, 70));
        fsFar = new FuzzySet("Far", new TrapezoidalFunction(50, 70, TrapezoidalFunction.EdgeType.Left));
        lvDistance = new LinguisticVariable("Distance", 0, 100);
        lvDistance.AddLabel(fsNear);
        lvDistance.AddLabel(fsMedi);
        lvDistance.AddLabel(fsFar);

    }

    private void SetSpeedFuzzySets()
    {
        fsSlow = new FuzzySet("Slow", new TrapezoidalFunction(30, 50, TrapezoidalFunction.EdgeType.Right));
        fsMedium = new FuzzySet("Medium", new TrapezoidalFunction(30, 50, 80, 100));
        fsFast = new FuzzySet("Fast", new TrapezoidalFunction(80,100, TrapezoidalFunction.EdgeType.Left));
        lvSpeed = new LinguisticVariable("Speed", 0, 100);
        lvSpeed.AddLabel(fsSlow);
        lvSpeed.AddLabel(fsMedium);
        lvSpeed.AddLabel(fsFast);
    } 

    private void AddToDatabase()
    {
        database = new Database();
        database.AddVariable(lvDistance);
        database.AddVariable(lvSpeed);
        Rules();
        
    }

    private void Rules()
    {
        infSystem = new InferenceSystem(database, new CentroidDefuzzifier(120));
        infSystem.NewRule("Rule 1", "IF Distance IS Far THEN Speed IS Slow"); //AITank Player tanka uzaksa yavaş
        infSystem.NewRule("Rule 2", "IF Distance IS Medi THEN Speed IS Medium"); // AITank Player tanka orta uzaklıkta ise orta hızda
        infSystem.NewRule("Rule 3", "IF Distance IS Near THEN Speed IS Fast"); //AITank Player tanka yakınsa hızlı gidecek
    }

    void Update()
    {
        Evaluate();
    }

    private void Evaluate()
    {
        Vector3 dir = (Player.position - transform.position);
        distance = dir.magnitude;
        dir.Normalize();
        infSystem.SetInput("Distance", distance);
        speed = infSystem.Evaluate("Speed");
        transform.position += dir * speed * Time.deltaTime * 0.010f;
        /*
        distance = Vector3.Distance(Player.position, transform.position);
        infSystem.SetInput("Distance", distance);
        speed = infSystem.Evaluate("Speed");
        */
        Debug.Log(speed);
    }
}
