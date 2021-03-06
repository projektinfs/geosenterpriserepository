﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;

namespace GeosEnterprise.Repositories
{
    public class ComputersRepository : BaseRepository<Computer>
    {
        public static Computer GetByRepairId(int repairId)
        {
            return ExecuteQuery(() =>
            {
                var repair = Repositories.RepairsRepository.GetById(repairId);
                return Where(p => p.ID == repair.ComputerID).FirstOrDefault();
            });
        }
    }
}
