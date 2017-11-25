using System;

namespace OptimizationLibrary
{
    public sealed class SenderWrapper
    {
        public readonly Object OuterSender;

        public readonly Object InnerSender;

        public SenderWrapper(Object outerSender, Object innerSender)
        {
            OuterSender = outerSender;
            InnerSender = innerSender;
        }
    }
}