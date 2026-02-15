using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageManager
{
    public class GarageManagerPresenter : IGarageManager
    {
        GarageManagerModel _model;

        public GarageManagerPresenter(IGarageGrid garageGrid)
        {
            _model = new GarageManagerModel(garageGrid);

            Subscribe();
        }

        private void Subscribe()
        {
            _model.Grid.UpdateLayer += OnUpdateLayer;
        }

        private void OnUpdateLayer()
        {
            RefreshGarages?.Invoke();
        }

        public void AddTransport(ITransport transport, IGarage garage)
        {
            garage.AddTransport(transport);
        }

        public List<IGarage> GetGarages()
        {
            return _model.Grid.GetGarages().Values.ToList();
        }

        public bool FindGarage(string name, out IGarage iGarage)
        {
            iGarage = null;
            foreach(IGarage garage in GetGarages())
            {
                if(garage.GetName() == name)
                {
                    iGarage = garage;
                    return true;
                }
            }
            return false;
        }

        public event Action RefreshGarages;
    }
}
