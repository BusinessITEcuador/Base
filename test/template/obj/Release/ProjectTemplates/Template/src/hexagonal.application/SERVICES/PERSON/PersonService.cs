using AutoMapper;
using $safeprojectname$.models.person;
using hexagonal.domain.entities;
using hexagonal.domain.repositories.interfaces;
using $safeprojectname$.services.person.interfaces;
namespace $safeprojectname$.services.person
{
  public class PersonService : IPersonService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    public void Add(PersonRequestModel accountModel)
    {
      var repository = _unitOfWork.GetPersonRepository();
      PersonEntity account = _mapper.Map<PersonEntity>(accountModel);
      repository.AddSync(account);
      _unitOfWork.Save();
    }

    IList<PersonResponseModel> IPersonService.GetAll()
    {
      IPersonDomainRepository repository = _unitOfWork.GetPersonRepository();
      return this._mapper.Map<IList<PersonResponseModel>>(repository.GetAllSync());
    }
  }
}
