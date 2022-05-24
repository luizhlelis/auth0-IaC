using System.Threading.Tasks;
using Pulumi;

namespace PulumiSample
{
    class Program
    {
        static Task<int> Main() => Deployment.RunAsync<MyStack>();
    }
}
