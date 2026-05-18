using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TransportSlotModel
{
    private readonly TransportConfig _transportConfig;

    public TransportSlotModel(TransportConfig tc)
    {
        _transportConfig = tc;
    }

    public string GetName()
    {
        return _transportConfig.Name;
    }

    public int GetMaxSpeed()
    {
        return _transportConfig.MaxSpeed;
    }

    public int GetMaxCapacity()
    {
        return _transportConfig.MaxCapacity;
    }

    public int GetCost()
    {
        return _transportConfig.Cost;
    }

    public TransportConfig GetTransportConfig()
    {
        return _transportConfig;
    }
}
