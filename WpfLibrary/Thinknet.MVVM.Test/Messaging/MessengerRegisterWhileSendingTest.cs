namespace Thinknet.MVVM.Test.Messaging
{
    using System.Collections.Generic;

    using Thinknet.MVVM.Messaging;

    using NUnit.Framework;

    [TestFixture]
    public class MessengerRegisterWhileSendingTest
    {
        private const string TestContentString = "Hello world";
        private const string TestContentStringNested = "Hello earth";

        [Test]
        public void TestMessengerRegisteringForMessageBaseWhileSending()
        {
            Messenger.Reset();
            TestRecipient.Reset();

            var list = new List<TestRecipient2>();

            for (var index = 0; index < 10; index++)
            {
                list.Add(new TestRecipient2(true));
            }

            Messenger.Default.Send(new GenericMessage<string>(TestContentString));

            Assert.AreEqual(null, TestRecipient.LastReceivedString);
            Assert.AreEqual(0, TestRecipient.ReceivedStringMessages);

            Messenger.Default.Send(new GenericMessage<string>(TestContentStringNested));

            Assert.AreEqual(TestContentStringNested, TestRecipient.LastReceivedString);
            Assert.AreEqual(10, TestRecipient.ReceivedStringMessages);
        }

        [Test]
        public void TestMessengerRegisteringInlineWhileReceiving()
        {
            Messenger.Default.Register<string>(
                this, 
                m => Messenger.Default.Register<PropertyChangedMessage<string>>(this, m2 => { }));

            const string SentContent = "Hello world";
            Messenger.Default.Send(SentContent);
        }

        [Test]
        public void TestMessengerRegisteringInlineWhileReceivingMessageBase()
        {
            Messenger.Default.Register<string>(
                this, 
                true, 
                m => Messenger.Default.Register<PropertyChangedMessage<string>>(this, m2 => { }));

            const string SentContent = "Hello world";
            Messenger.Default.Send(SentContent);
        }

        [Test]
        public void TestMessengerRegisteringMessageBaseInlineWhileReceiving()
        {
            Messenger.Default.Register<string>(
                this, 
                m => Messenger.Default.Register<PropertyChangedMessage<string>>(this, true, m2 => { }));

            const string SentContent = "Hello world";
            Messenger.Default.Send(SentContent);
        }

        [Test]
        public void TestMessengerRegisteringMessageBaseInlineWhileReceivingMessageBase()
        {
            Messenger.Default.Register<string>(
                this, 
                true, 
                m => Messenger.Default.Register<PropertyChangedMessage<string>>(this, true, m2 => { }));

            const string SentContent = "Hello world";
            Messenger.Default.Send(SentContent);
        }

        [Test]
        public void TestMessengerRegisteringWhileSending()
        {
            Messenger.Reset();
            TestRecipient.Reset();

            var list = new List<TestRecipient1>();

            for (var index = 0; index < 10; index++)
            {
                list.Add(new TestRecipient1(true));
            }

            Messenger.Default.Send(new GenericMessage<string>(TestContentString));

            Assert.AreEqual(null, TestRecipient.LastReceivedString);
            Assert.AreEqual(0, TestRecipient.ReceivedStringMessages);

            Messenger.Default.Send(new GenericMessage<string>(TestContentStringNested));

            Assert.AreEqual(TestContentStringNested, TestRecipient.LastReceivedString);
            Assert.AreEqual(10, TestRecipient.ReceivedStringMessages);
        }

        [Test]
        public void TestMessengerUnregisteringFromMessageBaseWhileReceiving()
        {
            Messenger.Default.Register<string>(
                this, 
                true, 
                m => Messenger.Default.Unregister(this));

            Messenger.Default.Send("Hello world");
        }

        [Test]
        public void TestMessengerUnregisteringWhileReceiving()
        {
            Messenger.Default.Register<string>(
                this, 
                m => Messenger.Default.Unregister(this));

            Messenger.Default.Send("Hello world");
        }

        public abstract class TestRecipient
        {
            public static string LastReceivedString { get; protected set; }

            public static int ReceivedStringMessages { get; protected set; }

            public static void Reset()
            {
                LastReceivedString = null;
                ReceivedStringMessages = 0;
            }
        }

        public class TestRecipient1 : TestRecipient
        {
            public TestRecipient1(bool register)
            {
                if (register)
                {
                    Messenger.Default.Register<GenericMessage<string>>(this, ReceiveString);
                }
            }

            protected virtual void ReceiveString(GenericMessage<string> m)
            {
                Messenger.Default.Register<GenericMessage<string>>(this, ReceiveStringNested);
            }

            protected void ReceiveStringNested(GenericMessage<string> m)
            {
                ReceivedStringMessages++;
                LastReceivedString = m.Content;
            }
        }

        public class TestRecipient2 : TestRecipient
        {
            public TestRecipient2(bool register)
            {
                if (register)
                {
                    Messenger.Default.Register<MessageBase>(this, true, ReceiveString);
                }
            }

            public virtual void ReceiveString(MessageBase m)
            {
                var message = m as GenericMessage<string>;
                if (message != null)
                {
                    Messenger.Default.Register<MessageBase>(this, true, ReceiveStringNested);
                }
            }

            public void ReceiveStringNested(MessageBase m)
            {
                var message = m as GenericMessage<string>;
                if (message != null)
                {
                    ReceivedStringMessages++;
                    LastReceivedString = message.Content;
                }
            }
        }
    }
}
