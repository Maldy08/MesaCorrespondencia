using Microsoft.EntityFrameworkCore;

namespace MesaCorrespondencia.Server.Repositorios
{
    public class OficiosRepository : IOficiosRepository
    {
        private readonly DataContext _context;

        public OficiosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<OficioDTO>> CreateOficio(OficioDTO oficio)
        {
            var oficioAdd = new Oficio
            {
                Ejercicio = oficio.Ejercicio,
                Folio = oficio.Folio,
                Eor = oficio.Eor,
                Tipo = oficio.Tipo,
                NoOficio = oficio.NoOficio,
                Pdfpath = oficio.Pdfpath,
                Fecha = oficio.Fecha,
                FechaCaptura = oficio.FechaCaptura,
                FechaAcuse = oficio.FechaAcuse,
                FechaLimite = oficio.FechaLimite,
                RemDepen = oficio.RemDepen,
                RemSiglas = oficio.RemSiglas,
                RemNombre = oficio.RemNombre,
                RemCargo = oficio.RemCargo,
                DestDepen = oficio.DestDepen,
                DestSiglas = oficio.DestSiglas,
                DestNombre = oficio.DestNombre,
                DestCargo = oficio.DestCargo,
                Tema = oficio.Tema,
                Estatus = oficio.Estatus,
                Empqentrega = oficio.Empqentrega,
                Relacionoficio = oficio.Relacionoficio,
                Depto = oficio.Depto,
                DeptoRespon = oficio.DeptoRespon,

            };

            _context.Oficios.Add(oficioAdd);
            if (oficio.OficioBitacora != null)
                  _context.OficiosBitacoras.Add(oficio.OficioBitacora);

            if (oficio.OficiosResponsables != null && oficio.OficiosResponsables.Count > 0)
                    oficio.OficiosResponsables.ForEach(of => _context.OficiosResponsables.Add(of));
            try
            {
              await _context.SaveChangesAsync();
                //if(id == 3)
                //{

                //}
            }
            catch 
            {
                return new ServiceResponse<OficioDTO>
                {
                    Data = null,
                    Message = "Ocurrio un error al guardar el oficio.",
                    Success = false
                };
            }
            return new ServiceResponse<OficioDTO>
            {
                Data = oficio,
                Message = "Oficio registrdo exitosamente!"
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
    }
}
