﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;


namespace GeosEnterprise.Repositories
{
    public class RepairsRepository : BaseRepository<Repair, RepairDTO>
    {
        public new static void Update(RepairDTO repair)
        {
            repair.ClientID = repair.Client.ID;
            repair.ComputerID = repair.Computer.ID;
            repair.DealerID = repair.Dealer.ID;
            BaseRepository<Computer, ComputerDTO>.Update(repair.Computer);
            BaseRepository<Repair, RepairDTO>.Update(repair);
        }

        public new static IList<Repair> GetAllCurrent()
        {
            return BaseRepository<Repair>.GetAllCurrent().Where(p => p.RealizationDate == null).ToList();
        }

        
        public new static IList<Repair> GetAllCompleted()
        {
            return BaseRepository<Repair>.GetAllCurrent().Where(p => p.Status == DBO.RepairStatus.Completed).ToList();
        }
        

        public static Repair Add(Repair repair)
        {
            return ExecuteQuery(() =>
            {
                repair.Client = null;
                repair.Dealer = null;
                return Insert(repair);
            });
        }

        public static void Edit(Repair repair)
        {
            ExecuteQuery(() =>
            {
                Update(repair);
            });
        }

        public static void Edit(RepairDTO repair)
        {
            ExecuteQuery(() =>
            {
                Update(repair);
            });
        }

        public static void Delete(int id)
        {
            ExecuteQuery(() =>
            {
                Delete(GetById(id));
            });
        }
    }
}
