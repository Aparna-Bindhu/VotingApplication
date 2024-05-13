using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Data.Model;

namespace Voting.Data.Contracts
{
    public interface IJsonFileDataRepository<T>
    {
        List<T> GetAll();
        List<T> Add(List<T> entities, int id);
    }
}
