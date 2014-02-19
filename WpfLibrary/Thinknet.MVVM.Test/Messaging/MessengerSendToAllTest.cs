namespace Thinknet.MVVM.Test.Messaging
{
    using NUnit.Framework;

    using Thinknet.MVVM.Messaging;

    [TestFixture]
    public class MessengerSendToAllTest
    {
        public string StringContent1
        {
            get;
            private set;
        }

        public string StringContent2
        {
            get;
            private set;
        }

        [Test]
        public void TestSendingToAllRecipients()
        {
            const string testContent = "abcd";

            Reset();
            Messenger.Reset();

            Messenger.Default.Register<TestMessage>(this, m => StringContent1 = m.Content);

            Messenger.Default.Register<TestMessage>(this, m => StringContent2 = m.Content);

            var externalRecipient = new TestRecipient();
            externalRecipient.RegisterWith(Messenger.Default);

            Assert.AreEqual(null, StringContent1);
            Assert.AreEqual(null, StringContent2);
            Assert.AreEqual(null, externalRecipient.StringContent);

            Messenger.Default.Send(new TestMessage
            {
                Content = testContent
            });

            Assert.AreEqual(testContent, StringContent1);
            Assert.AreEqual(testContent, StringContent2);
            Assert.AreEqual(testContent, externalRecipient.StringContent);
        }

        [Test]
        public void TestSendingNullMessage()
        {
            Messenger.Reset();
            Messenger.Default.Send<NotificationMessage>(null);
        }

        //// Helpers

        private void Reset()
        {
            StringContent1 = null;
            StringContent2 = null;
        }

        public class TestMessage
        {
            public string Content
            {
                get;
                set;
            }
        }

        private class TestRecipient
        {
            public string StringContent
            {
                get;
                private set;
            }

            internal void RegisterWith(IMessenger messenger)
            {
                messenger.Register<TestMessage>(this, m => StringContent = m.Content);
            }
        }
    }
}