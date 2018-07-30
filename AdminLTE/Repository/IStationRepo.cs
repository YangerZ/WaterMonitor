using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLTE.Repository
{
    interface IStationRepo
    {
        IQueryable<TempEntities> SelectAll();
        SelectByName(string userName);
        void Add(TempEntities entity);
        bool Delete(int id);

    }
}
