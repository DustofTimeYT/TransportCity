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
        AddStorage(new Storage());
    }

    public void AddStorage(Storage storage)
    {
        _company_M.Storages.Add(storage);
    }

    public List<Storage> GetStorages()
    {  return _company_M.Storages; }
}
