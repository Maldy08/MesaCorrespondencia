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

        public async Task<ServiceResponse<Oficio>> CreateOficio(Oficio oficio)
        {
            _context.Oficios.Add(oficio);
            if(oficio.OficiosBitacoras!=null && oficio.OficiosBitacoras.Count > 0)
            {
                foreach (var bitacora in oficio.OficiosBitacoras)
                {
                    _context.OficiosBitacoras.Add(bitacora);
                }
            }

            if(oficio.OficiosResponsables!= null && oficio.OficiosResponsables.Count > 0)
            {
                foreach (var responsable in oficio.OficiosResponsables)
                {
                    _context.OficiosResponsables.Add(responsable);
                }
            }
            await _context.SaveChangesAsync();
            return new ServiceResponse<Oficio>
            {
                Data = oficio,
                Message = "Oficio registrdo exitosamente!"
            };

        }

        //public async Task<ServiceResponse<List<Oficio>>> GetAllOficios()
        //{
        //    var response = new ServiceResponse<List<Oficio>>
        //    {
        //        Data = await _context.Oficios.ToListAsync()
        //    };
        //    return response;
        //}

        //public async Task<ServiceResponse<Oficio>> GetOficioById(int ejercicio, int folio, int eor)
        //{
        //    var response = new ServiceResponse<Oficio>
        //    {
        //        Data = await _context.Oficios.FirstOrDefaultAsync(o  => o.Ejercicio == ejercicio
        //          && o.Folio == folio && o.Eor == eor)
        //    };
        //    return response;
        //}
    }
}
