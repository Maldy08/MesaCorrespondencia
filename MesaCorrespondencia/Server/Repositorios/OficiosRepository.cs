using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MesaCorrespondencia.Server.Repositorios
{
    public class OficiosRepository : IOficiosRepository
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OficiosRepository(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ServiceResponse<OficiosBitacora>> CreateBitacora(OficiosBitacora oficiosBitacora)
        {
            _context.OficiosBitacoras.Add(oficiosBitacora);
            try
            {
                await _context.SaveChangesAsync();
                return new ServiceResponse<OficiosBitacora>
                {
                    Data = oficiosBitacora,
                    Message = "Bitacora agregada exitosamente!"
                };
            }
            catch
            {
                return new ServiceResponse<OficiosBitacora>
                {
                    Data = null,
                    Message = "Ocurrio un error al procesar el registro"
                };
            }
        }

        public async Task<List<UploadResult>> UploadFiles(List<IFormFile> files)
        {
            List<UploadResult> uploadResults = new List<UploadResult>();
            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);
                trustedFileNameForFileStorage = Path.GetRandomFileName();
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "oficios", trustedFileNameForFileStorage);
                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);

                uploadResult.StoredFileName = trustedFileNameForFileStorage;
                uploadResults.Add(uploadResult);

            }
            return uploadResults;
        }

        public async Task<ServiceResponse<Oficio>> CreateOficio(Oficio oficio)
        {
            #region 

            //var oficioAdd = new Oficio
            //{
            //    Ejercicio = oficio.Ejercicio,
            //    Folio = oficio.Folio,
            //    Eor = oficio.Eor,
            //    Tipo = oficio.Tipo,
            //    NoOficio = oficio.NoOficio,
            //    Pdfpath = oficio.Pdfpath,
            //    Fecha = oficio.Fecha,
            //    FechaCaptura = oficio.FechaCaptura,
            //    FechaAcuse = oficio.FechaAcuse,
            //    FechaLimite = oficio.FechaLimite,
            //    RemDepen = oficio.RemDepen,
            //    RemSiglas = oficio.RemSiglas,
            //    RemNombre = oficio.RemNombre,
            //    RemCargo = oficio.RemCargo,
            //    DestDepen = oficio.DestDepen,
            //    DestSiglas = oficio.DestSiglas,
            //    DestNombre = oficio.DestNombre,
            //    DestCargo = oficio.DestCargo,
            //    Tema = oficio.Tema,
            //    Estatus = oficio.Estatus,
            //    Empqentrega = oficio.Empqentrega,
            //    Relacionoficio = oficio.Relacionoficio,
            //    Depto = oficio.Depto,
            //    DeptoRespon = oficio.DeptoRespon,

            //};
            #endregion
            _context.Oficios.Add(oficio);
            if (oficio.OficioBitacora != null)
                _context.OficiosBitacoras.Add(oficio.OficioBitacora);
            if (oficio.OficiosResponsables != null && oficio.OficiosResponsables.Count > 0)
                oficio.OficiosResponsables.ForEach(of => _context.OficiosResponsables.Add(of));
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return new ServiceResponse<Oficio>
                {
                    Data = null,
                    Message = $"Ocurrio un error al guardar el Folio.{oficio.Folio}",
                    Success = false
                };
            }
            return new ServiceResponse<Oficio>
            {
                Data = oficio,
                Message = $"Folio: {oficio.Folio}, ha sido registrado exitosamente!"
            };
        }

        public async Task<ServiceResponse<List<VwOficiosLista>>> GetAllOficios()
        {
            var response = new ServiceResponse<List<VwOficiosLista>>
            {
                Data = await _context.VwOficiosListas.ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<OficiosBitacora>>> GetBitacoraList(int ejercicio, int folio, int eor)
        {
            var response = new ServiceResponse<List<OficiosBitacora>>
            {
                Data = await _context.OficiosBitacoras
                             .Where(o => o.Ejercicio == ejercicio && o.Folio == folio && o.Eor == eor)
                             .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<OficiosEstatus>>> GetEstatus()
        {
            var response = new ServiceResponse<List<OficiosEstatus>>
            {
                Data = await _context.OficiosEstatuses
                 .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<OficiosEstatus>> GetEstatusById(int id, int eor)
        {
            var response = new ServiceResponse<OficiosEstatus>
            {
                Data = await _context.OficiosEstatuses
             .FirstOrDefaultAsync(o => o.IdEstatus == id && o.Eor == eor)

            };
            return response;
        }

        public async Task<ServiceResponse<List<VwOficiosLista>>> GetOficiosListaMc(int eor)
        {
            var response = new ServiceResponse<List<VwOficiosLista>>
            {
                Data = await _context.VwOficiosListas
                         .Where(of => of.Rol == 1 && of.Eor == eor)
                         .OrderByDescending(of => of.Folio)
                         .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<VwOficiosLista>>> GetOficiosListaUser(int ejercicio, int eor, int idEmpleado, int iddepto)
        {
            var response = new ServiceResponse<List<VwOficiosLista>>
            {
                Data = await _context.VwOficiosListas
                        .Where(of => of.Ejercicio == ejercicio && of.Eor == eor
                            && (of.Depto == iddepto && of.Rol == 1 || of.IdEmpleado == iddepto && of.Rol == 2))
                        .OrderByDescending(of => of.Folio)
                        .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<OficiosUsuext>>> GetOficiosUsuariosExternos()
        {
            var response = new ServiceResponse<List<OficiosUsuext>>
            {
                Data = await _context.OficiosUsuexts
                          .OrderBy(o => o.IdExterno)
                         .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<OficiosBitacora>> UpdateBitacora(OficiosBitacora oficiosBitacora)
        {
            var bitacoraUpdate = await _context.OficiosBitacoras.FindAsync(oficiosBitacora.Ejercicio, oficiosBitacora.Folio, oficiosBitacora.Eor, oficiosBitacora.FechaCaptura);
            if (bitacoraUpdate != null)
            {
                bitacoraUpdate.IdEmpleado = oficiosBitacora.IdEmpleado;
                bitacoraUpdate.Estatus = oficiosBitacora.Estatus;
                bitacoraUpdate.Comentarios = oficiosBitacora.Comentarios;
                try
                {
                    await _context.SaveChangesAsync();
                    return new ServiceResponse<OficiosBitacora>
                    {
                        Data = bitacoraUpdate,
                        Message = "Registro actualizado correcamente"
                    };
                }
                catch
                {
                    return new ServiceResponse<OficiosBitacora>
                    {
                        Data = null,
                        Success = false,
                        Message = "Ocurrio un error al actualizar el registro"
                    };
                }
            }
            else
            {
                return new ServiceResponse<OficiosBitacora>
                {
                    Data = null,
                    Success = false,
                    Message = "Registro no encontrado"
                };
            }
        }

        public async Task<ServiceResponse<Oficio>> UpdateOficio(Oficio oficio)
        {
            var oficioUpdate = await _context.Oficios.FindAsync(oficio.Ejercicio, oficio.Folio, oficio.Eor);
            if (oficioUpdate != null)
            {
                //oficioUpdate.Ejercicio = oficio.Ejercicio;
                //oficioUpdate.Folio = oficio.Folio;
                //oficioUpdate.Eor = oficio.Eor;
                oficioUpdate.Tipo = oficio.Tipo;
                oficioUpdate.NoOficio = oficio.NoOficio;
                oficioUpdate.Pdfpath = oficio.Pdfpath;
                oficioUpdate.Fecha = oficio.Fecha;
                //oficioUpdate.FechaCaptura = oficio.FechaCaptura;
                oficioUpdate.FechaAcuse = oficio.FechaAcuse;
                oficioUpdate.FechaLimite = oficio.FechaLimite;
                oficioUpdate.RemDepen = oficio.RemDepen;
                oficioUpdate.RemSiglas = oficio.RemSiglas;
                oficioUpdate.RemNombre = oficio.RemNombre;
                oficioUpdate.RemCargo = oficio.RemCargo;
                oficioUpdate.DestDepen = oficio.DestDepen;
                oficioUpdate.DestSiglas = oficio.DestSiglas;
                oficioUpdate.DestNombre = oficio.DestNombre;
                oficioUpdate.DestCargo = oficio.DestCargo;
                oficioUpdate.Tema = oficio.Tema;
                oficioUpdate.Estatus = oficio.Estatus;
                oficioUpdate.Empqentrega = oficio.Empqentrega;
                oficioUpdate.Relacionoficio = oficio.Relacionoficio;
                oficioUpdate.Depto = oficio.Depto;
                oficioUpdate.DeptoRespon = oficio.DeptoRespon;
                try
                {
                    await _context.SaveChangesAsync();
                    return new ServiceResponse<Oficio>
                    {
                        Data = oficioUpdate,
                        Message = "Registro actualizado correcamente"
                    };
                }
                catch
                {
                    return new ServiceResponse<Oficio>
                    {
                        Data = null,
                        Success = false,
                        Message = "Ocurrio un error al actualizar el registro"
                    };
                }
            }
            else
            {
                return new ServiceResponse<Oficio>
                {
                    Data = null,
                    Success = false,
                    Message = "Registro no encontrado"
                };
            }
        }

        public async Task<ServiceResponse<OficiosParametro>> GetParametros(int ejercicio)
        {
            var response = new ServiceResponse<OficiosParametro>
            {
                Data = await _context.OficiosParametros.FirstOrDefaultAsync(o => o.Ejercicio == ejercicio)
            };
            return response;
        }

        public async Task<ServiceResponse<Oficio>> UpdatePdfPath(Oficio oficio)
        {
            var oficioUpdate = await _context.Oficios.FindAsync(oficio.Ejercicio, oficio.Folio, oficio.Eor);
            if (oficioUpdate != null)
            {
                oficioUpdate.Pdfpath = oficio.Pdfpath;
                try
                {
                    await _context.SaveChangesAsync();
                    return new ServiceResponse<Oficio>
                    {
                        Data = oficioUpdate,
                        Message = "Registro actualizado correcamente"
                    };
                }
                catch (Exception)
                {

                    return new ServiceResponse<Oficio>
                    {
                        Data = null,
                        Success = false,
                        Message = "Registro no encontrado"
                    };
                }
            }
            else
            {
                return new ServiceResponse<Oficio>
                {
                    Data = null,
                    Success = false,
                    Message = "Registro no encontrado"
                };
            }
        }

        public async Task<ServiceResponse<VwOficiosLista>> GetOficioByFolio(int ejercicio, int eor, int folio)
        {
            var response = new ServiceResponse<VwOficiosLista>
            {
                Data = await _context.VwOficiosListas.FirstOrDefaultAsync(o => o.Ejercicio == ejercicio && o.Eor == eor && o.Folio == folio)
             };

            return response;
        }

        public async Task<ServiceResponse<OficiosUsuext>> CreateOficioUsuext(OficiosUsuext oficiosUsuext)
        {
            try
            {
                _context.OficiosUsuexts.Add(oficiosUsuext);

                await _context.SaveChangesAsync();
            }
            catch
            {
                return new ServiceResponse<OficiosUsuext>
                {
                    Data = oficiosUsuext,
                    Message = $"Ocurrio un error al guardar el Folio. {oficiosUsuext.IdExterno}",
                    Success = false
                };
            }
            return new ServiceResponse<OficiosUsuext>
            {
                Data = oficiosUsuext,
                Message = $"Folio: {oficiosUsuext.Nombre}, ha sido registrado exitosamente!"
            };
        }
    }
}
