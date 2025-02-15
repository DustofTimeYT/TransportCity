using System;
using System.Collections.Generic;


public class Trasport: Iweareable
{
    private string _name;
    private int _maxSpeed;
    private int _currentCost;
    private int _maxCargoCapacity;
    private int _duration;
    private List<Cargo> _currentCargo;

    public int GetDuration()
    {
        throw new NotImplementedException();
    }

    public void Repair()
    {
        throw new NotImplementedException();
    }
}
