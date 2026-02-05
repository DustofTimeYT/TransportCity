using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IConsumer
{
    public IReadOnlyList<string> GetConsumerMaterials();
}
