using StoreRepository.Entity.AccountEntity.RoleEnum;

namespace StoreRepository.Entity.AccountEntity
{
    public interface IGuest
    {
        Role GuestRole { get; set; }
    }
}