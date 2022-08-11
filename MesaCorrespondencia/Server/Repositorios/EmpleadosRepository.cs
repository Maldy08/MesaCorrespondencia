using Microsoft.EntityFrameworkCore;

namespace MesaCorrespondencia.Server.Repositorios
{
    public class EmpleadosRepository : IEmpleadosRepository
    {
        private readonly DataContext _context;

        public EmpleadosRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<VsEmpleadosSisco>>> GetAll()
        {
            var response = new ServiceResponse<List<VsEmpleadosSisco>>
            {
                Data = await _context.EmpleadosSisco
                  .Where(e => e.Activo =="V")
                  .OrderBy(e => e.Empleado)
                   .ToListAsync()
            };
            return response;
        }
        public async Task<ServiceResponse<List<VsEmpleadosSisco>>> GetByDepto(int depto)
        {
            var response = new ServiceResponse<List<VsEmpleadosSisco>>
            {
                Data = await _context.EmpleadosSisco
                        .Where(e => e.Deptoue == depto && e.Activo == "V")
                        .OrderBy(e => e.Empleado)
                        .ToListAsync()
            };
            return response;
        }
        public async Task<ServiceResponse<VsEmpleadosSisco>> GetById(int id)
        {
            var response = new ServiceResponse<VsEmpleadosSisco>
            {
                Data = await _context.EmpleadosSisco
                    .FirstOrDefaultAsync(e => e.Empleado == id)
            };
            return response;
        }
    }
}
