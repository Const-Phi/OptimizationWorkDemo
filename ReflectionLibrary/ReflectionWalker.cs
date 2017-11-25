using System;
using System.Text;

namespace ReflectionLibrary
{
    public static class ReflectionWalker
    {
        private static String GetFullPath(Object sender, String propertyName, StringBuilder tailBuffer)
        {
            if (sender is SenderWrapper wrapper)
                return GetFullPath(wrapper.InnerSender, propertyName,
                    tailBuffer.AppendLine(wrapper.OuterSender.GetType().FullName));
            
            var senderType = sender.GetType();
            var newValue = senderType.GetProperty(propertyName)?.GetValue(sender);
            tailBuffer.AppendLine(senderType.FullName);
            tailBuffer.AppendLine($"\tproperty \"{propertyName}\" changed with new value {newValue}.");
            return tailBuffer.ToString();
        }
        
        public static String GetFullPath(Object sender, String propertyName)
            => GetFullPath(sender, propertyName, new StringBuilder());
    }
}