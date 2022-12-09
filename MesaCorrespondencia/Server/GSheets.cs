using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace MesaCorrespondencia.Server
{
    public class GSheets
    {
        bool encontrado = false;
         string[] Scopes = { SheetsService.Scope.Spreadsheets };
        int contador = 1;
        int depto;
        public GoogleCredential  Gctredential;
        SheetsService  sheetsService;
        //ID de del Sheets
        private const string SpreadsheetId = "1Z9ME01O4_8t6xqctiXdANJVB0osxPOhYxSEfpAykNOY";
        /*
           Hoja 1  Nombre de la pestaña o hoja
           A:A     Rango de valores segun la columna  
        */
         string ReadRange = "Hoja 1!A:A";

        public GSheets(GoogleCredential  Gctredential, int depto)
        {
            this.Gctredential = Gctredential;
            this.depto = depto;

            GoogleCredential credential = Gctredential.CreateScoped(Scopes);

                // Create Drive API service.
                var service = new SheetsService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Sheets API "
                });
            sheetsService = service;
        }



        public async Task<String> ReadAsync()
        {
            IList < IList<object>> values;
            try
            {
                encontrado = false;
                var valuesResource = sheetsService.Spreadsheets.Values;
                try
                {
                    var response =  valuesResource.Get(SpreadsheetId, ReadRange);
                    values = response.Execute().Values;
                }
                catch(Exception e)
                {
                    return "response " + e.Message;
                }
                if (values == null || !values.Any())
                {
                    Console.WriteLine("No data found.");
                    return "error";
                }
                var header = string.Join(" ", values.First().Select(r => r.ToString()));
                Console.WriteLine($"Header: {header}");

                foreach (var row in values.Skip(1))
                {
                    var res = string.Join(" ", row.Select(r => r.ToString()));
                    Console.WriteLine(res);
                    contador ++;
                    if (res.CompareTo(depto.ToString())==0)
                    {
                       encontrado = true;

                        break;
                    }
                }
                
                return (encontrado ? UpdateCell(contador)+"": "Departamento no encontrado" ) ;
            }
            catch(Exception e)
            {
                return "no jalo" + e.Message;
            }
          }

        public String UpdateCell(int pos)
        {
            IList<IList<object>> values;
            var range = $"Hoja 1!B{pos}";
            var valuesResource = sheetsService.Spreadsheets.Values;
               
                    var response = valuesResource.Get(SpreadsheetId, range);
                    values = response.Execute().Values;

            var cambio = values.ElementAt(0).ElementAt(0).ToString();
            int conv = Int32.Parse(cambio);
            var valueRange = new ValueRange();

            var oblist = new List<object>() { (conv+1) };
            valueRange.Values = new List<IList<object>> { oblist };

            var updateRequest = sheetsService.Spreadsheets.Values.Update(valueRange, SpreadsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = updateRequest.Execute();

            return cambio;
        }
    }
}
