namespace DesafioNtt.Identity.Interfaces.UseCase.Identity
{
    public interface IUserCaseIdentity<TInput, TResult>
    {
        public Task<TResult> Execute(TInput userData);
    }
}