using System;

namespace test_Libreary
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = costa_rica_dotnet.IBANUtils.IBANOperatorList;
            var result = costa_rica_dotnet.IBANUtils.verifyIBANFormat("CR97010200009260125504");
            Console.WriteLine(result);
        }
    }
}
