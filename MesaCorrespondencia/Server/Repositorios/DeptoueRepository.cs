
using Microsoft.EntityFrameworkCore;

namespace MesaCorrespondencia.Server.Repositorios
{
    public class DeptoueRepository : IDeptoueRepository
    {
        private readonly DataContext _context;

        public DeptoueRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<DeptosUe>>> GetAllDeptos()
        {
            var response = new ServiceResponse<List<DeptosUe>>
            {
                Data = await _context.DeptosUe.ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<DeptosUe>> GetDeptoById(int id)
        {
            var response = new ServiceResponse<DeptosUe>();
            var deptoue = await _context.DeptosUe.FirstOrDefaultAsync(d => d.IdCea == id);
            if(deptoue == null)
            {
                response.Success = false;
                response.Message = "No se encontro este departamento";
            }
            else
            {
                response.Data = deptoue;
            }

            return response;
        }
    }
}
