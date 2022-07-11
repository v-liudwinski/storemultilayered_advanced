using StoreRepository.Entity.AccountEntity.RoleEnum;

namespace StoreRepository.Entity.AccountEntity
{
    public interface IUser
    {
        string Login { get; set; }
        string Password { get; set; }
        Role UserRole { get; set; }
    }
}