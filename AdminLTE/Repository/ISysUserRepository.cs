using 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLTE.Repository
{
    interface IStationRepository
    {
        IQueryable<SysUser> SelectAll();
        SysUser SelectByName(string userName);
        void Add(SysUser sysUser);
        bool Delete(int id);
        
    }
}
