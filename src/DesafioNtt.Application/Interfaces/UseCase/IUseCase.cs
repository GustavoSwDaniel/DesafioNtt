namespace DesafioNtt.Application.Interfaces.UseCase
{
    public interface IUseCase<TInput, TResult>
    {
        public Task<TResult> Execute(TInput userData, string? email);
    }
}
