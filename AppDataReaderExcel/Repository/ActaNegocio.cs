using AppDataReaderExcel.Models;
using ExcelDataReader;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace AppDataReaderExcel.Repository
{
    public class ActaNegocio
    {
        private readonly ContextDataBase _context;

        public ActaNegocio(ContextDataBase context)
        {
            _context = context;
        }

        public async Task<bool> InsertDataToDatabase(List<Acta> data)
        {
           // string connectionString = _context.GetConnectionString("ProyectoConnection");

            try
            {
                await _context.acta.AddRangeAsync(data);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        //public List<Acta> ReadExcelData(IFormFile file)
        //{
        //    var data = new List<Acta>();

        //    using (var stream = file.OpenReadStream())
        //    {
        //        using (var reader = ExcelReaderFactory.CreateReader(stream))
        //        {
        //            // Saltar la primera fila (encabezados de columna)
        //            reader.Read();

        //            while (reader.Read())
        //            {
        //                var excelData = new Acta
        //                {
        //                    Id = reader.GetString(0),
        //                    NumeroActa = reader.GetString(1),
        //                    Departamento = reader.GetString(2),
        //                    CodigoCurso = reader.GetString(3),
        //                    Curso = reader.GetString(4),
        //                    HorasCredito = reader.GetString(5),
        //                    Area = reader.GetString(6),
        //                    Semestre = reader.GetString(7),
        //                    Anio = reader.GetString(8),
        //                    Nro = reader.GetString(9),
        //                    Codigo = reader.GetString(10),
        //                    ApellidosNombre = reader.GetString(11),
        //                    Nota = reader.GetString(12),
        //                    Observaciones = reader.GetString(13)
        //                    // Continúa con las demás columnas
        //                };

        //                data.Add(excelData);
        //            }
        //        }
        //    }

        //    return data;
        //}

        public List<Acta> ReadExcelData(Stream stream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<Acta> actas = new List<Acta>();

            using (var package = new ExcelPackage(stream))
            {
                if (package.Workbook.Worksheets.Count > 0)
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

                    int rowCount = worksheet.Dimension.Rows;


                    for (int row = 2; row <= rowCount; row++)
                    {
                        var acta = new Acta
                        {
                            Id = GetValueFromCell(worksheet, row, 1),
                            NumeroActa = GetValueFromCell(worksheet, row, 2),
                            Departamento = GetValueFromCell(worksheet, row, 3),
                            CodigoCurso = GetValueFromCell(worksheet, row, 4),
                            Curso = GetValueFromCell(worksheet, row, 5),
                            HorasCredito = GetValueFromCell(worksheet, row, 6),
                            Area = GetValueFromCell(worksheet, row, 7),
                            Semestre = GetValueFromCell(worksheet, row, 8),
                            Anio = GetValueFromCell(worksheet, row, 9),
                            Nro = GetValueFromCell(worksheet, row, 10),
                            Codigo = GetValueFromCell(worksheet, row, 11),
                            ApellidosNombre = GetValueFromCell(worksheet, row, 12),
                            Nota = GetValueFromCell(worksheet, row, 13),
                            Observaciones = GetValueFromCell(worksheet, row, 14)
                        };

                        actas.Add(acta);
                    }
                }
                else
                {
                    throw new InvalidOperationException("El archivo Excel no contiene ninguna hoja de cálculo.");
                }
            }

            return actas;
        }
        public string GetValueFromCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue != null ? cellValue.ToString() : null;
        }


    }
}
