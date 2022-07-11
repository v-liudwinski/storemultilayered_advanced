using System;
using System.Collections.Generic;
using System.Text;

namespace StoreRepository.Repository.RepositoryInterface
{
    interface IRepository<T>
    {
        List<T> FindAll();
    }
}
