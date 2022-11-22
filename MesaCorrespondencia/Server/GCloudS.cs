using Google.Apis.Auth.OAuth2;
using Google.Apis.Docs.v1;
using Google.Apis.Docs.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
    // Class to demonstrate use of Docs get documents API
   public class GCloudS
    {
        private GoogleCredential credencial;
        public GCloudS(){
        }
  String fileId = "1NVUavkhHQTYSslMudpeg5pJDOWskPBxnc8Sjkm-O7lc";
  String nombre = "miDocu";
  String mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
public GCloudS(GoogleCredential stream){
credencial = stream;

}

  public  MemoryStream DriveExportWord()
        {
            try
            {
                /* Load pre-authorized user credentials from the environment.
                 TODO(developer) - See https://developers.google.com/identity for 
                 guides on implementing OAuth2 for your application. */
                GoogleCredential credential = credencial.CreateScoped(DriveService.Scope.Drive);

                // Create Drive API service.
                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Drive API Snippets"
                });
              var copReq =   service.Files.Copy(fileMetadata(),fileId).Execute();
         Console.WriteLine(copReq.Id);
         documentUpdate(copReq.Id);
               var request = service.Files.Export(copReq.Id,mime);
            
            var stream = new MemoryStream();
                // Add a handler which will be notified on progress changes.
                // It will notify on each chunk download and when the
                // download is completed or failed.
                request.MediaDownloader.ProgressChanged +=
                    progress =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                            {
                                Console.WriteLine(progress.BytesDownloaded);
                                break;
                            }
                            case DownloadStatus.Completed:
                            {
                                Console.WriteLine("Download complete.");
                                break;
                            }
                            case DownloadStatus.Failed:
                            {
                                Console.WriteLine("Download failed.");
                                break;
                            }
                        }
                    };
                request.Download(stream); 
                                stream.Seek(0, SeekOrigin.Begin);//set position to beginning    


       //    var requestD = service.Files.Delete(copReq.Id);
     //      requestD.SupportsAllDrives = true;
        //   requestD.Execute();
                return stream;
            }
            catch (Exception e)
            {
                // TODO(developer) - handle error appropriately
                if (e is AggregateException)
                {
                    Console.WriteLine("Credential Not found");
                }
                else
                {
                                    Console.WriteLine(e.Message);

                    throw;
                }
            }
            return null;
        }
    
   private void documentUpdate(String documentId)
        {
        String DEPENDENCIA = "COMISIÓN ESTATAL DEL AGUA DE BAJA CALIFORNIA";
        String SECCION = "DIRECCION JURIDICA Y DE NORMATIVIDAD";
        String OFICIO = "39/2.1/048/2021";
        String FECHA = DateTime.Now.ToString("yyyy/MM/dd");
        String ASUNTO = "FORMATO PARA OFICIOS";
        String RESPONSABLE = "Alejandro Ramos Hernandez";
        String PUESTO = "Practicante";




            try
            {
                var credential = credencial.CreateScoped(DocsService.Scope.Documents );;


                // Create Google Docs API service.
                var service = new DocsService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Google Docs API .NET Quickstart"
                });
  
            //actualizando campos de la plantilla
        IList<Request> requests = new List<Request>();
        requests.Add(new Request{ReplaceAllText = (new ReplaceAllTextRequest{
            ContainsText = (new SubstringMatchCriteria{
                Text = ("{{DEPENDENCIA}}"),
                MatchCase=(true)}),
                ReplaceText= DEPENDENCIA})});
        requests.Add(new Request{
                ReplaceAllText=(new ReplaceAllTextRequest{
                        ContainsText=(new SubstringMatchCriteria{
                        Text=("{{SECCION}}")
                        ,MatchCase=(true)})
                        ,ReplaceText= SECCION})});
            requests.Add(new Request
            {
                ReplaceAllText = (new ReplaceAllTextRequest
                {
                    ContainsText = (new SubstringMatchCriteria
                    {
                        Text = ("{{OFICIO}}") ,
                        MatchCase = (true)
                    }),
                    ReplaceText = OFICIO
                })
            });
            requests.Add(new Request
            {
                ReplaceAllText = (new ReplaceAllTextRequest
                {
                    ContainsText = (new SubstringMatchCriteria
                    {
                        Text = ("{{FECHA}}"),
                        MatchCase = (true)
                    }),
                    ReplaceText = FECHA
                })
            });
            requests.Add(new Request
            {
                ReplaceAllText = (new ReplaceAllTextRequest
                {
                    ContainsText = (new SubstringMatchCriteria
                    {
                        Text = ("{{ASUNTO}}"),
                        MatchCase = (true)
                    }),
                    ReplaceText = ASUNTO
                })
            });
            requests.Add(new Request
            {
                ReplaceAllText = (new ReplaceAllTextRequest
                {
                    ContainsText = (new SubstringMatchCriteria
                    {
                        Text = ("{{RESPONSABLE}}"),
                        MatchCase = (true)
                    }),
                    ReplaceText = RESPONSABLE
                })
            });
            requests.Add(new Request
            {
                ReplaceAllText = (new ReplaceAllTextRequest
                {
                    ContainsText = (new SubstringMatchCriteria
                    {
                        Text = ("{{SECCION}}")
                        ,
                        MatchCase = (true)
                    })
                        ,
                    ReplaceText = SECCION
                })
            });
            requests.Add(new Request
            {
                ReplaceAllText = (new ReplaceAllTextRequest
                {
                    ContainsText = (new SubstringMatchCriteria
                    {
                        Text = ("{{PUESTO}}"),
                        MatchCase = (true)
                    }),
                    ReplaceText = PUESTO
                })
            });

            BatchUpdateDocumentRequest body = new BatchUpdateDocumentRequest();
           body.Requests=requests;
        service.Documents.BatchUpdate(body, documentId).Execute();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }


 /// <summary>
 /// Crear copia de la plantilla
 /// </summary>
 /// <returns>File docs generado (ID Documento)</returns>
    private Google.Apis.Drive.v3.Data.File fileMetadata(){
        return   new Google.Apis.Drive.v3.Data.File()
            {
                Name = "oficio_"+"alejandro"+"_"+DateTime.Now.ToString("dd/MM/yyyy"),
                Parents = new List<string>
                    {
                        "13oNtrIExmCN6Oy6pJQ9KpzEBDbwmMflZ" //FolderID
                    }
            };
    }

    }