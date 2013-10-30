namespace Turn5.WebServices
{
    public interface ITestService
    {
        TestServiceResponse<T> GetResponse<T>();
    }
}
