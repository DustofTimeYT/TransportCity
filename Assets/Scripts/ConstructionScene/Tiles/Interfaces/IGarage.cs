using System.Collections.Generic;

public interface IGarage
{
    public List<ITransport> GetTransports();

    public void AddTransport(ITransport transport);

    public string GetName();
}
