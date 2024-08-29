public class Script : ScriptBase
{
    // This test code simplifies code for testing a connector that returns null values 
    // from a SQL Queries to a data warehouse. The underlying Warehouse API can return "null" or null
    // depending on an API setting.
    public override async Task<HttpResponseMessage> ExecuteAsync()
    {
        return GetObjects();
    }

    private HttpResponseMessage GetObjects()
    {
        JObject nullTestObject = new JObject();
        nullTestObject.Add(new JProperty("STRING_NULL", "null"));
        nullTestObject.Add(new JProperty("REAL_NULL", JValue.CreateNull()));

        JArray nullArray = new JArray();
        nullArray.Add(nullTestObject);
        var result = new MyResponse
        {
            Data = nullArray
        };

        var responseObj = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = CreateJsonContent(JsonConvert.SerializeObject(result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include }))
        };

        return responseObj;
    }

    public class MyResponse
    {
        public object? Data { get; set; }
    }
}
