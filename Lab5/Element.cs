using System;
using System.Collections.Generic;
using System.Xml.Linq;

public class Element
{
    private string _name;
    private string _latinName;
    private string _symbol;
    private float _atomicMass;


    public Element(string name, string latinName, string symbol, float atomicMass)
    {
        Name = name;
        LatinName = latinName;
        Symbol = symbol;
        AtomicMass = atomicMass;
       
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string LatinName
    {
        get { return _latinName; }
        set { _latinName = value; }
    }

    public string Symbol
    {
        get { return _symbol; }
        set { _symbol = value; }
    }

    public float AtomicMass
    {
        get { return _atomicMass; }
        set { _atomicMass = value; }
    }
}