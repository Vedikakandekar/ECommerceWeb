using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository
{
    public class CompanySettingsRepository : Repository<CompanySettings>, ICompanySettingsRepository
    {
        private ApplicationDbContext _db;
        public CompanySettingsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(CompanySettings entity)
        {
            _db.CompanySettings.Update(entity);
        }
    }
}
