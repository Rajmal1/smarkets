using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Models.Repository {
    public class ConsultaRepository {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public ConsultaRepository (IConfiguration configuration) {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString ("DbConnection");
        }

        public async Task<IEnumerable<ConsultaModel>> Select () {
            IEnumerable<ConsultaModel> consulta;

            string sSql = "SELECT * FROM TB_CONSULTA";

            using (IDbConnection db = new SqlConnection (stringConnection)) {
                consulta = await db.QueryAsync<ConsultaModel> (sSql);
            }

            if (consulta.AsList ().Count == 0)
                return null;
            return consulta;
        }

        public async Task<ConsultaModel> SelectId (long id) {
            ConsultaModel consulta;

            string sSql = $@"SELECT * FROM TB_CONSULTA
                            WHERE Id = {id}";

            using (IDbConnection db = new SqlConnection (stringConnection)) {
                consulta = await db.QueryFirstOrDefaultAsync<ConsultaModel> (sSql);
            }

            return consulta;
        }

        public async Task<ConsultaModel> Insert (ConsultaModel model) {
            IEnumerable<long> insertRow;

            string sSql = $@"INSERT INTO TB_CONSULTA (Paciente, DtInicioConsulta, DtFimConsulta, Observacao)
                            VALUES ('{model.Paciente}', '{model.DtInicioConsulta.ToString("yyyy-MM-dd HH:mm:ss")}', '{model.DtFimConsulta.ToString("yyyy-MM-dd HH:mm:ss")}', '{model.Observacao}')
                            SELECT SCOPE_IDENTITY()";

            using (IDbConnection db = new SqlConnection (stringConnection)) {
                insertRow = await db.QueryAsync<long> (sSql, model);
            }

            if (insertRow == null || insertRow.AsList ().Count == 0)
                return null;

            model.Id = insertRow.AsList () [0];
            return model;
        }

        public async Task<long> Update (ConsultaModel model, long id) {
            long updateRow;

            string sSql = $@"UPDATE TB_CONSULTA
                            SET DtInicioConsulta='{model.DtInicioConsulta.ToString("yyyy-MM-dd HH:mm:ss")}',DtFimConsulta='{model.DtFimConsulta.ToString("yyyy-MM-dd HH:mm:ss")}', Observacao = '{model.Observacao}'
                            WHERE Id = {id}";

            using (IDbConnection db = new SqlConnection (stringConnection)) {
                updateRow = await db.ExecuteAsync (sSql);
            }

            return updateRow;
        }

        public async Task<long> Delete (long id) {
            long deleteRow;

            string sSql = $@"DELETE FROM TB_CONSULTA
                            WHERE Id = {id}";
            using (IDbConnection db = new SqlConnection (stringConnection)) {
                deleteRow = await db.ExecuteAsync (sSql);
            }

            return deleteRow;
        }

        public async Task<ConsultaModel> SelectConsultaExistente (DateTime DtInicio, DateTime DtFim) {
            ConsultaModel consulta;

            string sSql = $@"SELECT * FROM TB_CONSULTA
                            WHERE DtInicioConsulta BETWEEN '{DtInicio}' AND '{DtFim}'";

            using (IDbConnection db = new SqlConnection (stringConnection)) {
                consulta = await db.QueryFirstOrDefaultAsync<ConsultaModel> (sSql);
            }

            return consulta;
        }
    }
}