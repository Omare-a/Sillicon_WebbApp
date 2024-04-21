using WebbApp.Models;

namespace WebbApp.ViewModels;

public class AccountDetailViewModel
{

    public AccountDetailsBasicInfoModel BasicInfo { get; set; } = new AccountDetailsBasicInfoModel();

    public AccountDetailsAddressInfoModel AddressInfo { get; set; } = new AccountDetailsAddressInfoModel();
}
