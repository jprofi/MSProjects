namespace Thinknet.MVVM.Test.Messaging
{
    using System;

    using NUnit.Framework;

    using Thinknet.MVVM.Messaging;
    using Thinknet.MVVM.Test.Helper.TestHelperClasses;

    /// <summary>
    ///     Tests garbage collection on messenger.
    /// </summary>
    [TestFixture]
    public class GarbageCollectionTest
    {
        private TestRecipient _recipient;
        private TestRecipientInternal _recipientInternal;
        private TestRecipientPrivate _recipientPrivate;
        private WeakReference _recipientReference;

        public void Reset()
        {
            _recipient = null;
            _recipientInternal = null;
            _recipientPrivate = null;
            _recipientReference = null;
        }

        [Test]
        public void TestGarbageCollectionForAnonymousMethod()
        {
            Messenger.Reset();

            _recipient = new TestRecipient(WeakActionTestCase.AnonymousMethod);
            _recipientReference = new WeakReference(_recipient);

            Assert.AreEqual(null, _recipient.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipient.Content);

            _recipient = null;
            GC.Collect();
            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForAnonymousMethodInternal()
        {
            Messenger.Reset();

            _recipientInternal = new TestRecipientInternal(WeakActionTestCase.AnonymousMethod);
            _recipientReference = new WeakReference(_recipientInternal);

            Assert.AreEqual(null, _recipientInternal.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipientInternal.Content);

            _recipientInternal = null;
            GC.Collect();
            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForAnonymousMethodPrivate()
        {
            Messenger.Reset();

            _recipientPrivate = new TestRecipientPrivate(WeakActionTestCase.AnonymousMethod);
            _recipientReference = new WeakReference(_recipientPrivate);

            Assert.AreEqual(null, _recipientPrivate.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipientPrivate.Content);

            _recipientPrivate = null;
            GC.Collect();
            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForAnonymousStaticMethod()
        {
            Messenger.Reset();
            TestRecipient.Reset();

            _recipient = new TestRecipient(WeakActionTestCase.AnonymousStaticMethod);
            _recipientReference = new WeakReference(_recipient);

            Assert.AreEqual(null, TestRecipient.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipient.ContentStatic);

            _recipient = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForAnonymousStaticMethodInternal()
        {
            Messenger.Reset();
            TestRecipientInternal.Reset();

            _recipientInternal = new TestRecipientInternal(WeakActionTestCase.AnonymousStaticMethod);
            _recipientReference = new WeakReference(_recipientInternal);

            Assert.AreEqual(null, TestRecipientInternal.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipientInternal.ContentStatic);

            _recipientInternal = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForAnonymousStaticMethodPrivate()
        {
            Messenger.Reset();
            TestRecipientPrivate.Reset();

            _recipientPrivate = new TestRecipientPrivate(WeakActionTestCase.AnonymousStaticMethod);
            _recipientReference = new WeakReference(_recipientPrivate);

            Assert.AreEqual(null, TestRecipientPrivate.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipientPrivate.ContentStatic);

            _recipientPrivate = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedInternalMethod()
        {
            Messenger.Reset();

            _recipient = new TestRecipient(WeakActionTestCase.InternalNamedMethod);
            _recipientReference = new WeakReference(_recipient);

            Assert.AreEqual(null, _recipient.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipient.Content);

            _recipient = null;
            GC.Collect();
            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedInternalMethodInternal()
        {
            Messenger.Reset();

            _recipientInternal = new TestRecipientInternal(WeakActionTestCase.InternalNamedMethod);
            _recipientReference = new WeakReference(_recipientInternal);

            Assert.AreEqual(null, _recipientInternal.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipientInternal.Content);

            _recipientInternal = null;
            GC.Collect();
            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedInternalMethodPrivate()
        {
            Messenger.Reset();

            _recipientPrivate = new TestRecipientPrivate(WeakActionTestCase.InternalNamedMethod);
            _recipientReference = new WeakReference(_recipientPrivate);

            Assert.AreEqual(null, _recipientPrivate.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipientPrivate.Content);

            _recipientPrivate = null;
            GC.Collect();
            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedInternalStaticMethod()
        {
            Messenger.Reset();
            TestRecipient.Reset();

            _recipient = new TestRecipient(WeakActionTestCase.InternalStaticMethod);
            _recipientReference = new WeakReference(_recipient);

            Assert.AreEqual(null, TestRecipient.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipient.ContentStatic);

            _recipient = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedInternalStaticMethodInternal()
        {
            Messenger.Reset();
            TestRecipientInternal.Reset();

            _recipientInternal = new TestRecipientInternal(WeakActionTestCase.InternalStaticMethod);
            _recipientReference = new WeakReference(_recipientInternal);

            Assert.AreEqual(null, TestRecipientInternal.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipientInternal.ContentStatic);

            _recipientInternal = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedInternalStaticMethodPrivate()
        {
            Messenger.Reset();
            TestRecipientPrivate.Reset();

            _recipientPrivate = new TestRecipientPrivate(WeakActionTestCase.InternalStaticMethod);
            _recipientReference = new WeakReference(_recipientPrivate);

            Assert.AreEqual(null, TestRecipientPrivate.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipientPrivate.ContentStatic);

            _recipientPrivate = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedMethod()
        {
            Messenger.Reset();

            _recipient = new TestRecipient(WeakActionTestCase.PublicNamedMethod);
            _recipientReference = new WeakReference(_recipient);

            Assert.AreEqual(null, _recipient.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipient.Content);

            _recipient = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedMethodInternal()
        {
            Messenger.Reset();

            _recipientInternal = new TestRecipientInternal(WeakActionTestCase.PublicNamedMethod);
            _recipientReference = new WeakReference(_recipientInternal);

            Assert.AreEqual(null, _recipientInternal.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipientInternal.Content);

            _recipientInternal = null;
            GC.Collect();
            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedMethodPrivate()
        {
            Messenger.Reset();
            TestRecipientPrivate.Reset();

            _recipientPrivate = new TestRecipientPrivate(WeakActionTestCase.PublicNamedMethod);
            _recipientReference = new WeakReference(_recipientPrivate);

            Assert.AreEqual(null, _recipientPrivate.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipientPrivate.Content);

            _recipientPrivate = null;
            GC.Collect();
            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedPrivateMethod()
        {
            Messenger.Reset();

            _recipient = new TestRecipient(WeakActionTestCase.PrivateNamedMethod);
            _recipientReference = new WeakReference(_recipient);

            Assert.AreEqual(null, _recipient.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipient.Content);

            _recipient = null;
            GC.Collect();
            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedPrivateMethodInternal()
        {
            Messenger.Reset();

            _recipientInternal = new TestRecipientInternal(WeakActionTestCase.PrivateNamedMethod);
            _recipientReference = new WeakReference(_recipientInternal);

            Assert.AreEqual(null, _recipientInternal.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipientInternal.Content);

            _recipientInternal = null;
            GC.Collect();
            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedPrivateMethodPrivate()
        {
            Messenger.Reset();

            _recipientPrivate = new TestRecipientPrivate(WeakActionTestCase.PrivateNamedMethod);
            _recipientReference = new WeakReference(_recipientPrivate);

            Assert.AreEqual(null, _recipientPrivate.Content);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, _recipientPrivate.Content);

            _recipientPrivate = null;
            GC.Collect();
            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedPrivateStaticMethod()
        {
            Messenger.Reset();
            TestRecipient.Reset();

            _recipient = new TestRecipient(WeakActionTestCase.PrivateStaticMethod);
            _recipientReference = new WeakReference(_recipient);

            Assert.AreEqual(null, TestRecipient.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipient.ContentStatic);

            _recipient = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedPrivateStaticMethodInternal()
        {
            Messenger.Reset();
            TestRecipientInternal.Reset();

            _recipientInternal = new TestRecipientInternal(WeakActionTestCase.PrivateStaticMethod);
            _recipientReference = new WeakReference(_recipientInternal);

            Assert.AreEqual(null, TestRecipientInternal.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipientInternal.ContentStatic);

            _recipientInternal = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedPrivateStaticMethodPrivate()
        {
            Messenger.Reset();
            TestRecipientPrivate.Reset();

            _recipientPrivate = new TestRecipientPrivate(WeakActionTestCase.PrivateStaticMethod);
            _recipientReference = new WeakReference(_recipientPrivate);

            Assert.AreEqual(null, TestRecipientPrivate.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipientPrivate.ContentStatic);

            _recipientPrivate = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedStaticMethod()
        {
            Messenger.Reset();
            TestRecipient.Reset();

            _recipient = new TestRecipient(WeakActionTestCase.PublicStaticMethod);
            _recipientReference = new WeakReference(_recipient);

            Assert.AreEqual(null, TestRecipient.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipient.ContentStatic);

            _recipient = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedStaticMethodInternal()
        {
            Messenger.Reset();
            TestRecipientInternal.Reset();

            _recipientInternal = new TestRecipientInternal(WeakActionTestCase.PublicStaticMethod);
            _recipientReference = new WeakReference(_recipientInternal);

            Assert.AreEqual(null, TestRecipientInternal.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipientInternal.ContentStatic);

            _recipientInternal = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        [Test]
        public void TestGarbageCollectionForNamedStaticMethodPrivate()
        {
            Messenger.Reset();
            TestRecipientPrivate.Reset();

            _recipientPrivate = new TestRecipientPrivate(WeakActionTestCase.PublicStaticMethod);
            _recipientReference = new WeakReference(_recipientPrivate);

            Assert.AreEqual(null, TestRecipientPrivate.ContentStatic);
            Assert.IsTrue(_recipientReference.IsAlive);

            const string message = "Hello world";

            Messenger.Default.Send(message);

            Assert.AreEqual(message, TestRecipientPrivate.ContentStatic);

            _recipientPrivate = null;
            GC.Collect();

            Assert.IsFalse(_recipientReference.IsAlive);
        }

        public class TestRecipient
        {
            public TestRecipient(WeakActionTestCase testCase)
            {
                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuff);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffPrivate);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffInternal);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffStatic);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffPrivateStatic);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffInternalStatic);
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            msg => Content = msg);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            msg => ContentStatic = msg);
                        break;
                }
            }

            public static string ContentStatic { get; private set; }
            public string Content { get; private set; }
            
            internal static string ContentInternalStatic { get; private set; }
            internal string ContentInternal { get; private set; }

            private static string ContentPrivateStatic { get; set; }
            private string ContentPrivate { get; set; }

            public static void DoStuffStatic(string message)
            {
                ContentStatic = message;
            }

            public static void Reset()
            {
                ContentStatic = null;
                ContentPrivateStatic = null;
                ContentInternalStatic = null;
            }

            public void DoStuff(string message)
            {
                Content = message;
            }

            internal static void DoStuffInternalStatic(string message)
            {
                ContentStatic = message;
                ContentInternalStatic = message;
            }

            internal void DoStuffInternal(string message)
            {
                Content = message;
                ContentInternal = message;
            }

            private static void DoStuffPrivateStatic(string message)
            {
                ContentStatic = message;
                ContentPrivateStatic = message;
            }

            private void DoStuffPrivate(string message)
            {
                Content = message;
                ContentPrivate = message;
            }
        }

        internal class TestRecipientInternal
        {
            public TestRecipientInternal(WeakActionTestCase testCase)
            {
                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuff);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffPrivate);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffInternal);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffStatic);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffPrivateStatic);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffInternalStatic);
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            msg => Content = msg);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            msg => ContentStatic = msg);
                        break;
                }
            }

            public static string ContentStatic { get; private set; }
            public string Content { get; private set; }
            
            internal static string ContentInternalStatic { get; private set; }
            internal string ContentInternal { get; private set; }

            private static string ContentPrivateStatic { get; set; }
            private string ContentPrivate { get; set; }

            public static void DoStuffStatic(string message)
            {
                ContentStatic = message;
            }

            public static void Reset()
            {
                ContentStatic = null;
                ContentPrivateStatic = null;
                ContentInternalStatic = null;
            }

            public void DoStuff(string message)
            {
                Content = message;
            }

            internal static void DoStuffInternalStatic(string message)
            {
                ContentStatic = message;
                ContentInternalStatic = message;
            }

            internal void DoStuffInternal(string message)
            {
                Content = message;
                ContentInternal = message;
            }

            private static void DoStuffPrivateStatic(string message)
            {
                ContentStatic = message;
                ContentPrivateStatic = message;
            }

            private void DoStuffPrivate(string message)
            {
                Content = message;
                ContentPrivate = message;
            }
        }

        private class TestRecipientPrivate
        {
            public TestRecipientPrivate(WeakActionTestCase testCase)
            {
                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuff);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffPrivate);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffInternal);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffStatic);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffPrivateStatic);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            DoStuffInternalStatic);
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            msg => Content = msg);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        Messenger.Default.Register<string>(
                            this, 
                            msg => ContentStatic = msg);
                        break;
                }
            }

            public static string ContentStatic { get; private set; }
            public string Content { get; private set; }

            internal static string ContentInternalStatic { get; private set; }
            internal string ContentInternal { get; private set; }

            private static string ContentPrivateStatic { get; set; }
            private string ContentPrivate { get; set; }

            public static void DoStuffStatic(string message)
            {
                ContentStatic = message;
            }

            public static void Reset()
            {
                ContentStatic = null;
                ContentPrivateStatic = null;
                ContentInternalStatic = null;
            }

            public void DoStuff(string message)
            {
                Content = message;
            }

            internal static void DoStuffInternalStatic(string message)
            {
                ContentStatic = message;
                ContentInternalStatic = message;
            }

            internal void DoStuffInternal(string message)
            {
                Content = message;
                ContentInternal = message;
            }

            private static void DoStuffPrivateStatic(string message)
            {
                ContentStatic = message;
                ContentPrivateStatic = message;
            }

            private void DoStuffPrivate(string message)
            {
                Content = message;
                ContentPrivate = message;
            }
        }
    }
}
