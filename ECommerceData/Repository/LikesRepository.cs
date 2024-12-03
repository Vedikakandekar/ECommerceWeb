using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository
{
    public class LikesRepository:Repository<Likes>, ILikesRepository
    {
        private ApplicationDbContext _db;
    public LikesRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public void Update(Likes entity)
    {
        _db.Likes.Update(entity);
    }
}
}
