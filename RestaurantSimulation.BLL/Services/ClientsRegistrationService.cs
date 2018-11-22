﻿using RestaurantSimulation.BLL.Models;
using RestaurantSimulation.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation.BLL.Services
{
    public class ClientsRegistrationService
    {
        EFUnitOfWork UnitOfWork;

        public WaiterService WaiterService { get; private set; } 
        public List<Table> Tables { get; private set; } 
        
        public List<ClientsService> ServiceClients { get; set; }

        public ClientsRegistrationService()
        {
            UnitOfWork = new EFUnitOfWork();

            Tables = new List<Table> { new Table { SeatcCount = 2, TableNumber = 1, Reserved = false },
                new Table { SeatcCount = 2, TableNumber = 2, Reserved = false },
                new Table { SeatcCount = 3, TableNumber = 3, Reserved = false },
                new Table { SeatcCount = 3, TableNumber = 4, Reserved = false } };
            
            WaiterService = new WaiterService();
            
        }

        public ClientsService AddClients(int VisitorsNumber)
        {
            // Find table whith nearest seats count
            var ClosestTable = Tables.Where(item => item.Reserved == false).OrderBy(item => Math.Abs(VisitorsNumber - item.SeatcCount)).First();

            // Indicate that the table is busy
            Tables.Where(item => item.TableNumber == ClosestTable.TableNumber).Select(u =>{ u.Reserved = true; return u; }).ToList();


            ClientsService Clients = new ClientsService();
            Clients.TableNumber = ClosestTable.TableNumber;
            Clients.WaiterService = WaiterService;

            return Clients;          
        }

        public void RemoveClients(int VisitorsNumber)
        {            
            
        }
    }
}