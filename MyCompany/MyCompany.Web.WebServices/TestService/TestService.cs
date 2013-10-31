namespace MyCompany.WebServices
{
    public class TestService : ITestService
    {
        public TestServiceResponse<T> GetResponse<T>()
        {
            var response = new TestServiceResponse<T>();

            response.RawResponseString = "Raw Response";

            return response;
        }
    }
}
