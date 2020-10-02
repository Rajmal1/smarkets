using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Services.Interfaces {
    public interface IConsultaService {
        Task<IEnumerable<ConsultaModel>> GetAll ();
        Task<ConsultaModel> GetId (long id);
        Task<ConsultaModel> Post (ConsultaModel model);
        Task<long> Put (ConsultaModel model, long id);
        Task<long> Delete (long id);
    }
}