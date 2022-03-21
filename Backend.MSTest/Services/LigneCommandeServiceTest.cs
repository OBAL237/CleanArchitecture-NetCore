using AutoMapper;
using AutoMapper.Configuration;
using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.ApplicationCore.Mapper;
using Backend.ApplicationCore.Services;
using Backend.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace Backend.MSTest
{
    [TestClass]
    public class LigneCommandeServiceTest 
    {
        private readonly ILigneCommandeRepository _LigneCommandeRepository;
        private readonly ILogger<LigneCommandeService> _log;
        private readonly IMapper _mapper;

        public LigneCommandeServiceTest()
        {
            _log = Substitute.For<ILogger<LigneCommandeService>>();
            _LigneCommandeRepository = Substitute.For<ILigneCommandeRepository>();

            var mapperCfg = new MapperConfigurationExpression();
            mapperCfg.AddProfile<AutoMapperProfile>();
            _mapper = new Mapper(new MapperConfiguration(mapperCfg));


        }

        [TestMethod]
        public void GetPrice_passing_listecommande_with_empty_elt_Return_1()
        {
            List<LigneCommande> list = new List<LigneCommande>();

            LigneCommandeService ligneCommandeService = new LigneCommandeService(_log, _mapper, _LigneCommandeRepository);

            var result = ligneCommandeService.GetPrice(list);

            Assert.AreEqual(result.Result, 1);
        }

        [TestMethod]
        public void GetPrice_passing_listecommande_with_one_elt_Return_1()
        {
            List<LigneCommande> list = new List<LigneCommande>
            {
                new LigneCommande()
            };

            LigneCommandeService ligneCommandeService = new LigneCommandeService(_log, _mapper, _LigneCommandeRepository);

            var result = ligneCommandeService.GetPrice(list);

            Assert.AreEqual(result.Result, 1);
        }

        [TestMethod]
        public void GetPrice_passing_listecommande_with_many_elt_Return_Fibonacci_number()
        {
            List<LigneCommande> list = new List<LigneCommande>
            {
                new LigneCommande{ 
                    Prix = 5
                },
                new LigneCommande{ 
                    Prix = 3
                },
                new LigneCommande{
                    Prix = 2
                },
                 new LigneCommande{
                    Prix = 1
                }
            };

            LigneCommandeService ligneCommandeService = new LigneCommandeService(_log, _mapper, _LigneCommandeRepository);

            var result = ligneCommandeService.GetPrice(list);

            Assert.AreEqual(result.Result, 8);
        }

    }
}
