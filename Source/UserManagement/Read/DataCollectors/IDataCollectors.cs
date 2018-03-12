using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read.DataCollectors
{
    public interface IDataCollectors
    {
        DataCollector GetById(Guid id);

        Task<DataCollector> GetByIdAsync(Guid id);
        IEnumerable<DataCollector> GetAll();

        Task<IEnumerable<DataCollector>> GetAllAsync();

        void Remove(Guid id);
        Task RemoveAsync(Guid id);

        void Save(DataCollector dataCollector);

        Task SaveAsync(DataCollector dataCollector);
    }
}