using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.SceneManagement;

public class Company_P
{
    private readonly Company_M _company_M;
    public Company_P() 
    {
        _company_M = new Company_M();
        //AddIGarage(new IGarage());
    }

    public void AddIGarage(IGarage igarage)
    {
        _company_M.garages.Add(igarage);
    }

    public List<IGarage> GetGarages()
    {  return _company_M.garages; }
}
