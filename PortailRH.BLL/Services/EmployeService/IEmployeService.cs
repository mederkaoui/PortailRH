﻿using PortailRH.BLL.Dtos.Employe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.EmployeService
{
    /// <summary>
    /// IEmployeService
    /// </summary>
    public interface IEmployeService
    {
        public Task<EmployeSelectListsDto> GetSelectListsData();

        public Task AddedEmploye(NouvelEmployeDto employe);
    }
}
