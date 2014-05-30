using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityMVC.Repository
{
    interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Update(T elemT);
        void Insert(T elemT);
        void Delete(object id);
    }
}
