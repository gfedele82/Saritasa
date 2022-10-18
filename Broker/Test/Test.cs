using Contracts.Engine;
using Contracts.Providers;
using DataAccess.Interfaces;
using Engine;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Models.Request;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class Test
    {
        private IBrokerEngine _brokerengine;
        private Mock<IValidator<RequestRates>> _mockValidatorBroker;
        private Mock<IRateProviderEngine> _mockRateProvider;
        private Mock<ILogger<BrokerEngine>> _mockLoggerBroker;
        private Mock<IRatesRepository> _mockRepository;
        private ValidationResult _validationResult;


        [SetUp]
        public void Setup()
        {
            _mockValidatorBroker = new Mock<IValidator<RequestRates>>();
            _mockRateProvider = new Mock<IRateProviderEngine>();
            _mockLoggerBroker = new Mock<ILogger<BrokerEngine>>();
            _mockRepository = new Mock<IRatesRepository>();
            _validationResult = new ValidationResult();

            _brokerengine = new BrokerEngine(_mockValidatorBroker.Object,
                _mockRepository.Object,
                _mockRateProvider.Object,
                _mockLoggerBroker.Object);
        }

        [TestCase("09/01/2022","09/10/2022",100)]
        public async Task TestRevenue(DateTime start, DateTime end, decimal money)
        {
            DateTime buyDate = DateTime.Parse("09/08/2022");
            DateTime sellDate = DateTime.Parse("09/09/2022");
            decimal revenue = 0.3962811846619166514422589M;
            _mockValidatorBroker.Setup(x => x.ValidateAsync(It.IsAny<RequestRates>(), It.IsAny<CancellationToken>())).ReturnsAsync(_validationResult);
            _mockRepository.Setup(x => x.GetByCiteria(p => p.Id >= start && p.Id <= end)).ReturnsAsync(GetData());
            var resp = await _brokerengine.GetBestRate(start, end, money);

            Assert.IsTrue(resp.buyDate == buyDate);
            Assert.IsTrue(resp.sellDate == sellDate);
            Assert.IsTrue(resp.revenue == revenue);
        }

        [TestCase("12/15/2014", "12/23/2014", 100)]
        public async Task TestRevenueExample(DateTime start, DateTime end, decimal money)
        {
            DateTime buyDate = DateTime.Parse("12/16/2014");
            DateTime sellDate = DateTime.Parse("12/22/2014");
            decimal revenue = 27.24205914567360350492880613M;
            _mockValidatorBroker.Setup(x => x.ValidateAsync(It.IsAny<RequestRates>(), It.IsAny<CancellationToken>())).ReturnsAsync(_validationResult);
            _mockRepository.Setup(x => x.GetByCiteria(p => p.Id >= start && p.Id <= end)).ReturnsAsync(GetDataExample());
            var resp = await _brokerengine.GetBestRate(start, end, money);

            Assert.IsTrue(resp.buyDate == buyDate);
            Assert.IsTrue(resp.sellDate == sellDate);
            Assert.IsTrue(resp.revenue == revenue);
        }

        public IEnumerable<DataAccess.Schema.Rates> GetData()
        {
            List<DataAccess.Schema.Rates> _list = new List<DataAccess.Schema.Rates>();

            _list.Add(new DataAccess.Schema.Rates()
            { 
                Id = DateTime.Parse("09/01/2022"),
                RUB = 60.209995M,
                EUR = 1.004843M,
                GBP = 0.86601M,
                JPY = 140.0825M,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("09/02/2022"),
                RUB = 60.274994M,
                EUR = 1.004722M,
                GBP = 0.86847M,
                JPY = 140.211M,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("09/03/2022"),
                RUB = 60.274994M,
                EUR = 1.004722M,
                GBP = 0.86847M,
                JPY = 140.211M,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("09/04/2022"),
                RUB = 60.209995M,
                EUR = 1.009235M,
                GBP = 0.871653M,
                JPY = 140.5345M,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("09/05/2022"),
                RUB = 61.010002M,
                EUR = 1.005001M,
                GBP = 0.86469M,
                JPY = 140.4355M,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("09/06/2022"),
                RUB = 61.275M,
                EUR = 1.010407M,
                GBP = 0.868762M,
                JPY = 143.145M,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("09/07/2022"),
                RUB = 60.800103M,
                EUR = 1.000202M,
                GBP = 0.867883M,
                JPY = 144.09857143M,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("09/08/2022"),
                RUB = 61.500005M,
                EUR = 0.999017M,
                GBP = 0.868101M,
                JPY = 143.87872222M,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("09/09/2022"),
                RUB = 60.785101M,
                EUR = 0.98526M,
                GBP = 0.862292M,
                JPY = 142.50505376M,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("09/10/2022"),
                RUB = 60.785101M,
                EUR = 0.985076M,
                GBP = 0.862495M,
                JPY = 142.50505376M,
                Response = "Test"
            });

            return _list.AsEnumerable();
        }

        public IEnumerable<DataAccess.Schema.Rates> GetDataExample()
        {
            List<DataAccess.Schema.Rates> _list = new List<DataAccess.Schema.Rates>();

            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("12/15/2014"),
                RUB = 60.17M,
                EUR = 0,
                GBP = 0,
                JPY = 0,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("12/16/2014"),
                RUB = 72.99M,
                EUR = 0,
                GBP = 0,
                JPY = 0,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("12/17/2014"),
                RUB = 66.01M,
                EUR = 0,
                GBP = 0,
                JPY = 0,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("12/18/2014"),
                RUB = 61.44M,
                EUR = 0,
                GBP = 0,
                JPY = 0,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("12/19/2014"),
                RUB = 59.79M,
                EUR = 0,
                GBP = 0,
                JPY = 0,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("12/20/2014"),
                RUB = 59.79M,
                EUR = 0,
                GBP = 0,
                JPY = 0,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("12/21/2014"),
                RUB = 59.79M,
                EUR = 0,
                GBP = 0,
                JPY = 0,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("12/22/2014"),
                RUB = 54.78M,
                EUR = 0,
                GBP = 0,
                JPY = 0,
                Response = "Test"
            });
            _list.Add(new DataAccess.Schema.Rates()
            {
                Id = DateTime.Parse("12/23/2014"),
                RUB = 54.80M,
                EUR = 0,
                GBP = 0,
                JPY = 0,
                Response = "Test"
            });

            return _list.AsEnumerable();
        }
    }
}
