using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CardStore_M
{
    private readonly TransportConfig _transportConfig;

    public CardStore_M(TransportConfig tc)
    {
        _transportConfig = tc;
    }

    public string GetName()
    {
        return _transportConfig.Name;
    }

    public TransportConfig GetTransportConfig()
    {
        return _transportConfig;
    }
}
