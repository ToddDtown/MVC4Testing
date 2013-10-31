namespace MyCompany.WebServices
{
    public class TestServiceResponse<T>
    {
        public T Value { get; set; }
        public string RawResponseString { get; set; }
    }
}
