using Microsoft.EntityFrameworkCore;

namespace MesaCorrespondencia.Server.Repositorios
{
    public class OficiosParametroRepository : IOficiosParametroRepository
    {
        private readonly DataContext _context;

        public OficiosParametroRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<OficiosParametro>> GetParametros(int ejercicio)
        {
            var response = new ServiceResponse<OficiosParametro>
            {
                Data = await _context.OficiosParametros.FirstOrDefaultAsync(p => p.Ejercicio == ejercicio)
            };
            return response;
        }


        public async Task<ServiceResponse<OficiosParametro>> UpdateParametros(OficiosParametro oficiosParametro)
        {
            var entity = await _context.OficiosParametros.FindAsync(oficiosParametro.Ejercicio);
            if (entity != null)
            {
                entity.NextFRec = oficiosParametro.NextFRec;
                entity.NextFEnv = oficiosParametro.NextFEnv;
                entity.NextFXexp = oficiosParametro.NextFXexp;

            }
            try
            {
                await _context.SaveChangesAsync();
                return new ServiceResponse<OficiosParametro>
                {
                    Data = entity,
                    Message = "Registro actualizado correctamente"
                };
            }
            catch (Exception e)
            {

                return new ServiceResponse<OficiosParametro>
                {
                    Data = null,
                    Message = e.Message
                };
            }
   
        }
    }
}
