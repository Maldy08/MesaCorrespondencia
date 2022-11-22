using Microsoft.EntityFrameworkCore;

namespace MesaCorrespondencia.Server.Repositorios
{
    public class FunctionsRepository : IFunctionsRepository
    {
        private readonly DataContext _context;

        public FunctionsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<GetDepartamentos>>> getDepartamentosF(int id)
        {
            var response = new ServiceResponse<List<GetDepartamentos>>
            {
                Data = await _context.GetDepartamentos.FromSqlInterpolated($"select * from table(GetDeptos({id}))").ToListAsync()
            };
            return response;
        }
    }
}
