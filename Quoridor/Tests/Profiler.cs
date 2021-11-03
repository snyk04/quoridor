using System;
using System.Diagnostics;

namespace Quoridor.Tests
{
    public static class Profiler
    {
        public static float TimeMethod(Action method)
        {
            var stopWatch = new Stopwatch();
            
            stopWatch.Start();
            method();
            stopWatch.Stop();

            return stopWatch.ElapsedMilliseconds;
        }
    }
}
