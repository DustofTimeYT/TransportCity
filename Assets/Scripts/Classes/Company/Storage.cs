using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport;

public class Storage
{
    private List<Transport_P> _transports;

    public Storage()
    {
        _transports = new List<Transport_P>();
    }

    public void AddTransport(Transport_P transport) => _transports.Add(transport);
}
