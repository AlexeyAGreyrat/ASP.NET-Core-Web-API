using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepositoryGet<T> where T : class
    {
        IList<T> GetInTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime);

        void Create(T item);

        T GetLast();

        IList<T> GetFromToByAgent(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);

        T GetLastFromAgent(int agentId);
    }
}
