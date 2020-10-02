using System;

namespace Models {
    public class ConsultaModel {
        public long Id { get; set; }
        public string Paciente { get; set; }
        public DateTime DtInicioConsulta { get; set; }
        public DateTime DtFimConsulta { get; set; }
        public string Observacao { get; set; }
    }
}