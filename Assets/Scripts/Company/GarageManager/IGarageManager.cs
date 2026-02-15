using System;
using System.Collections.Generic;

public interface IGarageManager
{
    public List<IGarage> GetGarages();

    public void AddTransport(ITransport transport, IGarage garage);

    public bool FindGarage(string name, out IGarage iGarage);

    public event Action RefreshGarages;
}