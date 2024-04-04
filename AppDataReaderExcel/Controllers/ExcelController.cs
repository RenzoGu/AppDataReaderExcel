using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using AppDataReaderExcel.Models;
using AppDataReaderExcel.Repository;

namespace AppDataReaderExcel.Controllers
{
    public class ExcelController : Controller
    {
        public ExcelController(ActaNegocio dataProcessor)
        {
            _dataProcessor = dataProcessor;
        }

        private readonly ActaNegocio _dataProcessor;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
           
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    var data = _dataProcessor.ReadExcelData(stream);

                    if (data.Count > 0)
                    {
                        if (await _dataProcessor.InsertDataToDatabase(data))
                        {
                            ViewBag.Message = "¡Archivo cargado y datos insertados en la base de datos con éxito!";
                        }
                        else
                        {
                            ViewBag.Message = "Error al insertar los datos en la base de datos.";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "No se encontraron datos en el archivo Excel.";
                    }
                } 
                
            }
            else
            {
                ViewBag.Message = "Por favor, seleccione un archivo Excel.";
            }

            return View("Index");
        }
    }
}
