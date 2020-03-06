using Common;
using System;

namespace EU.HelloWord
{
    [Utility("Hello world!")]
    public class HelloWorld : UtilityBase
    {
        public override string Run()
        {
            return "Hello world!";
        }

        public override string Help()
        {
            return "This utility just says 'Hello wolrd!'";
        }
    }
}
