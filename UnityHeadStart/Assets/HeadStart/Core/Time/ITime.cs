using System.Collections.Generic;
using Assets.HeadStart.Time;

namespace Assets.HeadStart.Core
{
    public interface ITime
    {
        void RxWait(InternalWaitCallback internalWaitCallback, float? seconds = null);
        void Wait(InternalWaitCallback internalWaitCallback, float? seconds = null);
        void AsyncForEach(int length, AsyncForEachCallbackIndex asyncForEach, float? seconds = null);
        void AsyncForEach(int length, AsyncForEachCallback asyncForEach, float? seconds = null);
        void AsyncForEach<T>(List<T> list, AsyncForEachCallbackT asyncForEach, float? seconds = null);
        void AsyncForEach<T>(T[] array, AsyncForEachCallbackT asyncForEach, float? seconds = null);
    }
}
