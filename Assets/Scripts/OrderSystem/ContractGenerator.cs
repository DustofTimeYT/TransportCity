using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Contract;

namespace ContractSystem
{
    public class ContractGenerator
    {
        private IProducerGrid _producerGrid;

        private IConsumerGrid _consumerGrid;

        public ContractGenerator(IProducerGrid producerGrid, IConsumerGrid consumerGrid)
        {
            _producerGrid = producerGrid;
            _consumerGrid = consumerGrid;
        }

        public bool GenerateContract(out ContractPresenter contract)
        {
            var producers = _producerGrid.GetProducers().Values.ToList();
            var consumers = _consumerGrid.GetConsumers().Values.ToList();

            return TryGenerateContract(producers, consumers, out contract);
        }

        public bool GenerateContracts(int amountContracts, out List<ContractPresenter> contracts)
        {
            var producers = _producerGrid.GetProducers().Values.ToList();
            var consumers = _consumerGrid.GetConsumers().Values.ToList();

            contracts = new List<ContractPresenter>();
            for (int i = 0; i == amountContracts; i++)
            {
                if (TryGenerateContract(producers, consumers, out var contract))
                {
                    contracts.Add(contract);
                }
            }

            if(contracts.Count == 0) return false;

            return true;
        }
        private bool TryGenerateContract(List<IProducer> producers, List<IConsumer> consumers, out ContractPresenter contract)
        {

            contract = null;

            if (producers.Count == 0 || consumers.Count == 0)
            {
                return false;
            }

            IProducer producer = producers[Random.Range(0, producers.Count - 1)];

            var products = producer.GetProducerMaterials();

            if (products.Count == 0)
            {
                return false;
            }

            ProductType product = products[Random.Range(0, products.Count - 1)];

            if (FindRelevantConsumer(consumers, product, out List<IConsumer> relevantConsumers))
            {
                IConsumer consumer = relevantConsumers[Random.Range(0, relevantConsumers.Count - 1)];
                int amount = Random.Range(1, 20);
                int moneyAmount = Random.Range(100, 2000);
                contract = new ContractPresenter(producer, consumer, product, amount, moneyAmount);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool FindRelevantConsumer(List<IConsumer> consumers, ProductType product, out List<IConsumer> relevantConsumers)
        {
            relevantConsumers = new();
            foreach (var consumer in consumers)
            {
                if (consumer.CheckConsumerMaterial(product))
                {
                    relevantConsumers.Add(consumer);
                }
                    
            }

            if (relevantConsumers.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
