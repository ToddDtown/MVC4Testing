namespace MyCompany.WebServices
{
    public interface ITestService
    {
        TestServiceResponse<T> GetResponse<T>();
    }
}
