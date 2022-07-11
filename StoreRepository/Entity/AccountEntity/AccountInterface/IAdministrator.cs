using StoreRepository.Entity.AccountEntity.RoleEnum;

namespace StoreRepository.Entity.AccountEntity
{
    public interface IAdministrator
    {
        Role AdminRole { get; set; }
        string Login { get; set; }
        string Password { get; set; }
    }
}