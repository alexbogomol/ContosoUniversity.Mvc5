using ContosoUniversity.Models;

namespace ContosoUniversity.DataAccess.Contracts
{
    public interface IInstructorsRepository : IRepository<Instructor>
    {
        Instructor GetByIdWithOffice(int id);
    }
}
