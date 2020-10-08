using AA.Dapper;
using AA.Dapper.Repositories;
using AA.Dapper.Test;
using AA.FrameWork.Domain;
using System;
using System.Collections.Generic;
using System.Text;


namespace AADemo.Domain.Repository
{
    public class VillageRepository:DapperRepository<Village>, IVillageRepository
    {
      
    }
}
