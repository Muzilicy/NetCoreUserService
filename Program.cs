// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using ServiceReference1;
using ServiceReference2;

Console.WriteLine("Hello, World!");

MesOperateClient client = new MesOperateClient();
var response = client.GetInfoAsync("123");
var result = response.Result;
var resultStr = JsonConvert.SerializeObject(result);

Console.WriteLine("result:" + resultStr);

ScadaOperateClient clientScada = new ScadaOperateClient();

var responseScada = clientScada.GetInfoAsync("123");

var resultScada = responseScada.Result;

var resultScadaStr = JsonConvert.SerializeObject(resultScada);

Console.WriteLine("resultScada:" + resultScadaStr);