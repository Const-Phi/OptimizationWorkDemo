using System;

namespace ReflectionLibrary
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