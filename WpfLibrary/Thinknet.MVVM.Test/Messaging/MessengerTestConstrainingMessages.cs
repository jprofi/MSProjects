namespace Thinknet.MVVM.Test.Messaging
{
    using System;

    using NUnit.Framework;

    using Thinknet.MVVM.Messaging;
    using Thinknet.MVVM.Test.Messaging;

    [TestFixture]
    public class MessengerTestConstrainingMessages
    {
        private static readonly string testContent = Guid.NewGuid().ToString();

        private bool _messageWasReceived;
        private bool _messageWasReceivedInITestMessage;
        private bool _messageWasReceivedInTestMessageBase;
        private bool _messageWasReceivedInMessageBase;

        [Test]
        public void TestConstrainingMessageByInterface()
        {
            Reset();
            Messenger.Reset();
            Messenger.Default.Register<ITestMessage>(this, ReceiveITestMessage);

            var testMessage = new TestMessageImpl(this)
            {
                Content = testContent
            };

            Assert.IsFalse(_messageWasReceived);
            Assert.IsFalse(_messageWasReceivedInTestMessageBase);
            Assert.IsFalse(_messageWasReceivedInMessageBase);
            Assert.IsFalse(_messageWasReceivedInITestMessage);
            Messenger.Default.Send(testMessage);
            Assert.IsFalse(_messageWasReceived);
            Assert.IsFalse(_messageWasReceivedInTestMessageBase);
            Assert.IsFalse(_messageWasReceivedInMessageBase);
            Assert.IsFalse(_messageWasReceivedInITestMessage);

            Messenger.Default.Unregister<ITestMessage>(this);
            Messenger.Default.Register<ITestMessage>(this, true, ReceiveITestMessage);

            Messenger.Default.Send(testMessage);
            Assert.IsTrue(_messageWasReceived);
            Assert.IsTrue(_messageWasReceivedInITestMessage);
            Assert.IsFalse(_messageWasReceivedInTestMessageBase);
            Assert.IsFalse(_messageWasReceivedInMessageBase);
        }

        [Test]
        public void TestConstrainingMessageByBaseClass()
        {
            Reset();
            Messenger.Reset();
            Messenger.Default.Register<TestMessageBase>(this, ReceiveTestMessageBase);

            var testMessage = new TestMessageImpl(this)
            {
                Content = testContent
            };

            Assert.IsFalse(_messageWasReceived);
            Assert.IsFalse(_messageWasReceivedInTestMessageBase);
            Assert.IsFalse(_messageWasReceivedInMessageBase);
            Assert.IsFalse(_messageWasReceivedInITestMessage);
            Messenger.Default.Send(testMessage);
            Assert.IsFalse(_messageWasReceived);
            Assert.IsFalse(_messageWasReceivedInTestMessageBase);
            Assert.IsFalse(_messageWasReceivedInMessageBase);
            Assert.IsFalse(_messageWasReceivedInITestMessage);

            Messenger.Default.Unregister<ITestMessage>(this);
            Messenger.Default.Register<TestMessageBase>(this, true, ReceiveTestMessageBase);

            Messenger.Default.Send(testMessage);
            Assert.IsTrue(_messageWasReceived);
            Assert.IsTrue(_messageWasReceivedInTestMessageBase);
            Assert.IsFalse(_messageWasReceivedInMessageBase);
            Assert.IsFalse(_messageWasReceivedInITestMessage);
        }

        [Test]
        public void TestConstrainingMessageByBaseClassAndReceiveWithInterface()
        {
            Reset();
            Messenger.Reset();
            Messenger.Default.Register<TestMessageBase>(this, ReceiveITestMessage);

            var testMessage = new TestMessageImpl(this)
            {
                Content = testContent
            };

            Assert.IsFalse(_messageWasReceived);
            Assert.IsFalse(_messageWasReceivedInTestMessageBase);
            Assert.IsFalse(_messageWasReceivedInMessageBase);
            Assert.IsFalse(_messageWasReceivedInITestMessage);
            Messenger.Default.Send(testMessage);
            Assert.IsFalse(_messageWasReceived);
            Assert.IsFalse(_messageWasReceivedInTestMessageBase);
            Assert.IsFalse(_messageWasReceivedInMessageBase);
            Assert.IsFalse(_messageWasReceivedInITestMessage);

            Messenger.Default.Unregister<ITestMessage>(this);
            Messenger.Default.Register<TestMessageBase>(this, true, ReceiveITestMessage);

            Messenger.Default.Send(testMessage);
            Assert.IsTrue(_messageWasReceived);
            Assert.IsFalse(_messageWasReceivedInTestMessageBase);
            Assert.IsFalse(_messageWasReceivedInMessageBase);
            Assert.IsTrue(_messageWasReceivedInITestMessage);
        }

        [Test]
        public void TestConstrainingMessageByBaseBaseClass()
        {
            Reset();
            Messenger.Reset();
            Messenger.Default.Register<MessageBase>(this, ReceiveMessageBase);

            var testMessage = new TestMessageImpl(this)
            {
                Content = testContent
            };

            Assert.IsFalse(_messageWasReceived);
            Assert.IsFalse(_messageWasReceivedInTestMessageBase);
            Assert.IsFalse(_messageWasReceivedInMessageBase);
            Assert.IsFalse(_messageWasReceivedInITestMessage);
            Messenger.Default.Send(testMessage);
            Assert.IsFalse(_messageWasReceived);
            Assert.IsFalse(_messageWasReceivedInTestMessageBase);
            Assert.IsFalse(_messageWasReceivedInMessageBase);
            Assert.IsFalse(_messageWasReceivedInITestMessage);

            Messenger.Default.Unregister<ITestMessage>(this);
            Messenger.Default.Register<MessageBase>(this, true, ReceiveMessageBase);

            Messenger.Default.Send(testMessage);
            Assert.IsTrue(_messageWasReceived);
            Assert.IsFalse(_messageWasReceivedInTestMessageBase);
            Assert.IsTrue(_messageWasReceivedInMessageBase);
            Assert.IsFalse(_messageWasReceivedInITestMessage);
        }

        public void ReceiveITestMessage(ITestMessage testMessage)
        {
            Assert.IsNotNull(testMessage);
            Assert.AreEqual(testContent, testMessage.Content);
            _messageWasReceived = true;
            _messageWasReceivedInITestMessage = true;
        }

        public void ReceiveTestMessageBase(TestMessageBase testMessage)
        {
            Assert.IsNotNull(testMessage);
            Assert.AreEqual(testContent, testMessage.Content);
            _messageWasReceived = true;
            _messageWasReceivedInTestMessageBase = true;
        }

        public void ReceiveMessageBase(MessageBase testMessage)
        {
            Assert.IsNotNull(testMessage);

            var castedMessage = testMessage as ITestMessage;

            if (castedMessage != null)
            {
                Assert.AreEqual(testContent, castedMessage.Content);
                _messageWasReceived = true;
                _messageWasReceivedInMessageBase = true;
            }
        }

        private void Reset()
        {
            _messageWasReceived = false;
            _messageWasReceivedInITestMessage = false;
            _messageWasReceivedInTestMessageBase = false;
            _messageWasReceivedInMessageBase = false;
        }
    }
}
