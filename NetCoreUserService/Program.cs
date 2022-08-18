// See https://aka.ms/new-console-template for more information
using NetCoreUserService;
using Newtonsoft.Json;
using ServiceReference1;
using ServiceReference2;
using static Weather.WeatherWebServiceSoapClient;

Console.WriteLine("Hello, World!");

#region 测试代码
//MesOperateClient client = new MesOperateClient();
//var response = client.GetInfoAsync("123");
//var result = response.Result;
//var resultStr = JsonConvert.SerializeObject(result);

//Console.WriteLine("result:" + resultStr);

//ScadaOperateClient clientScada = new ScadaOperateClient();

//var responseScada = clientScada.GetInfoAsync("123");

//var resultScada = responseScada.Result;

//var resultScadaStr = JsonConvert.SerializeObject(resultScada);

//Console.WriteLine("resultScada:" + resultScadaStr); 
#endregion


HttpPostWebService postWebService = new HttpPostWebService();

string url = "http://www.webxml.com.cn/WebServices/WeatherWebService.asmx";
string method = "getWeatherbyCityName";
var result1 = postWebService.PostService(url, method, "九江");

Console.WriteLine(JsonConvert.SerializeObject(result1));

Weather.WeatherWebServiceSoapClient webServiceSoapClient = new Weather.WeatherWebServiceSoapClient(EndpointConfiguration.WeatherWebServiceSoap);

Console.Write("请输入：");
string? input = Console.ReadLine();
while(input != "q")
{
    if (string.IsNullOrEmpty(input))
    {
        return;
    }
    string[] str = new string[23];

    str = await webServiceSoapClient.getWeatherbyCityNameAsync(input);

    if (str[8] == "")
    {
        Console.WriteLine("暂不支持你要查询的城市");
    }
    //Console.WriteLine("s[8]:" + s[8]);

    //Console.WriteLine("s[1] + s[6]:" + s[1] + " " + s[6]);

    //Console.WriteLine("s[10]: " + s[10]);
    string result = string.Join(",", str);

    Console.WriteLine(result);

    Console.WriteLine();

    Console.Write("请输入：");
    input = Console.ReadLine();
}
