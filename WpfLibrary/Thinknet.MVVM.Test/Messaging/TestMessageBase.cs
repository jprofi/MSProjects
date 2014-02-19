namespace Thinknet.MVVM.Test.Messaging
{
    using Thinknet.MVVM.Messaging;

    public class TestMessageBase : MessageBase, ITestMessage
    {
        public TestMessageBase(object sender)
            : base(sender)
        { 
        }

        public string Content
        {
            get;
            set;
        }
    }
}
