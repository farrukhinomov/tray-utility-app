using Common;
using System;

namespace EU.HiHowAreYou
{
    [Utility("How are you?")]
    public class ReturnHowAreYouMessage : UtilityBase
    {
        

        public override string Run()
        {
            return "Hey how are you?";
        }
        public override string Help()
        {
            return "This utility just says 'Hey how are you?'";
        }
    }
}
