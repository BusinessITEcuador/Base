using $safeprojectname$.models.person;

namespace $safeprojectname$.services.person.interfaces
{
  public interface IPersonService
  {
    public IList<PersonResponseModel> GetAll();
    public void Add(PersonRequestModel accountModel);
  }
}
