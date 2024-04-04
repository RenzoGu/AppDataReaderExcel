using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDataReaderExcel.Models
{
    public class Acta
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }
        [Column("nro_acta")]
        public string? NumeroActa { get; set; }
        [Column("departamento")]
        public string? Departamento { get; set; }
        [Column("codigo_curso")]
        public string? CodigoCurso { get; set; }
        [Column("curso")]
        public string? Curso { get; set; }
        [Column("horas_credito")]
        public string? HorasCredito { get; set; }
        [Column("area")]
        public string? Area { get; set; }
        [Column("semestre")]
        public string? Semestre { get; set; }
        [Column("anio")]
        public string? Anio { get; set; }
        [Column("nro")]
        public string? Nro { get; set; }
        [Column("codigo")]
        public string? Codigo { get; set; }
        [Column("apellidos_nombre")]
        public string? ApellidosNombre { get; set; }
        [Column("nota")]
        public string? Nota { get; set; }
        [Column("observaciones")]
        public string? Observaciones { get; set; }
    }
}
