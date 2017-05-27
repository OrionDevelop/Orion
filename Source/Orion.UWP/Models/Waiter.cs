using System;

namespace Orion.UWP.Models
{
    public static class Waiter
    {
        public static TimeSpan WaitSpan(int counter)
        {
            if (counter <= 0)
                throw new ArgumentException();
            var second = 1;
            for (var i = 0; i < counter; i++)
                second *= 2;
            return TimeSpan.FromSeconds(second);
        }
    }
}