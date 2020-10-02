using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Repository;
using Services.Interfaces;

namespace Services {
    public class ConsultaService : IConsultaService {

        private readonly ConsultaRepository _consultaModel;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger (typeof (ConsultaModel));

        public ConsultaService (ConsultaRepository consultaModel) {
            _consultaModel = consultaModel;
        }

        public async Task<long> Delete (long id) {
            try {
                var existentConsuta = await _consultaModel.SelectId (id);

                if (existentConsuta == null)
                    return -1;

                var deleteRow = await _consultaModel.Delete (id);

                return deleteRow;
            } catch (Exception ex) {
                log.Error ("ConsultaService.Delete: ", ex);
                throw ex;
            }
        }

        public async Task<IEnumerable<ConsultaModel>> GetAll () {
            try {
                var consultaList = await _consultaModel.Select ();

                return consultaList;
            } catch (Exception ex) {
                log.Error ("ConsultaService.GetAll: ", ex);
                throw ex;
            }
        }

        public async Task<ConsultaModel> GetId (long id) {
            try {
                var consulta = await _consultaModel.SelectId (id);

                return consulta;
            } catch (Exception ex) {
                log.Error ("ConsultaService.GetId: ", ex);
                throw ex;
            }
        }

        public async Task<ConsultaModel> Post (ConsultaModel model) {
            try {
                var existentConsulta = await _consultaModel.SelectConsultaExistente (model.DtInicioConsulta, model.DtFimConsulta);

                if (existentConsulta == null)
                    return null;

                var insertRow = await _consultaModel.Insert (model);

                return insertRow;
            } catch (Exception ex) {
                log.Error ("ConsultaService.Post: ", ex);
                throw ex;
            }
        }

        public async Task<long> Put (ConsultaModel model, long id) {
            try {
                var existentConsulta = await _consultaModel.SelectConsultaExistente (model.DtInicioConsulta, model.DtFimConsulta);

                var existentConsulta2 = await _consultaModel.SelectId (id);

                if (existentConsulta != null)
                    return -1;

                if (existentConsulta2 != null)
                    return -2;

                var updateRow = await _consultaModel.Update (model, id);

                return updateRow;
            } catch (Exception ex) {
                log.Error ("ConsultaService.Put: ", ex);
                throw ex;
            }
        }
    }
}