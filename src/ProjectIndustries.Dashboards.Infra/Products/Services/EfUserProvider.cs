using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.App.Identity.Model;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Infra.Services;

namespace ProjectIndustries.Dashboards.Infra.Products.Services
{
    public class EfUserProvider : DataProvider, IUserProvider
    {
        private readonly IMapper _mapper;
        private readonly IQueryable<User> _users;

        public EfUserProvider(DbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
            _users = GetDataSource<User>();
        }
        public async Task<UserData?> GetUserByIdAsync(long id, CancellationToken ct = default)
        {
            var user = await _users.FirstOrDefaultAsync(_ => _.Id == id, ct);
            return _mapper.Map<UserData>(user);
        }
    }
}