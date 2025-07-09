using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.Models;

namespace DNATestingSystem.Services.NhanVT
{
    public interface ISystemUserAccountService
    {
        Task<SystemUserAccount> GetUserAccount(string username, string password);
    }
}
