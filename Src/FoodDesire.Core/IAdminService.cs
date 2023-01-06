namespace FoodDesire.Core;
public interface IAdminService<Admin>: IUserService<Admin> where Admin : User { }
