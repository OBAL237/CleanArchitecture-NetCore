using AutoMapper;
using Backend.ApplicationCore.Interfaces.IRepositories;
using Backend.ApplicationCore.Interfaces.IServices;
using Backend.ApplicationCore.Services;
using Backend.Domain.Entities;
using Backend.Models;
using Mediator;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Backend.XunitTest
{
    public class UnitTest1
    {
        private readonly ILigneCommandeService _LigneCommandeService;
        private readonly ILigneCommandeRepository _LigneCommandeRepository;
        private readonly ILogger<LigneCommandeService> _log;
        private readonly ILogger<GetLigneCommandeListHandler> _logH;
        private readonly IMapper _mapper;

        public UnitTest1()
        {
            _log = new Mock<ILogger<LigneCommandeService>>().Object;
            _logH = new Mock<ILogger<GetLigneCommandeListHandler>>().Object;
            _mapper = new Mock<IMapper>().Object;
            _LigneCommandeRepository = new Mock<ILigneCommandeRepository>().Object;

            
        }

        [Fact]
        public void GetPrice_passing_listecommande_with_many_elt_Return_Fibonacci_number()
        {
            //Arrange
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

            var xy  = new List<LigneCommandeResponse>();
            xy.Add(new LigneCommandeResponse());
            xy.Add(new LigneCommandeResponse());
            var x = new Mock<ILigneCommandeService>();
            x.Setup(x => x.GetAllAsync()).ReturnsAsync(xy);
            GetLigneCommandeListHandler cmdHandler = new GetLigneCommandeListHandler(_logH, x.Object);

            //Act
            var result = ligneCommandeService.GetPrice(list);
            var result2 = cmdHandler.Handle(null, new CancellationToken()).Result;

            int i = 0;
            foreach (var item in result2)
            {
                i++;
            }


            //Assert
            Assert.Equal(8, result.Result);
           
            Assert.Equal(2,i);
        }
    }
}
