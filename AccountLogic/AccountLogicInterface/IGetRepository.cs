
using BusinessLogic.Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountLogic.AccountLogicInterface
{
    interface IGetRepository
    {
        public void GetRepository(Dictionary<string, IRepositoryEntity> repositories);
    }
}
