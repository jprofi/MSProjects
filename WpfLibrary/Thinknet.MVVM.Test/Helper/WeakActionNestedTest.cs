namespace Thinknet.MVVM.Test.Helper
{
    using System;

    using NUnit.Framework;

    using Thinknet.MVVM.Helper;
    using Thinknet.MVVM.Test.Helper.TestHelperClasses;

    [TestFixture]
    public class WeakActionNestedTest
    {
        private WeakAction _action;
        private InternalNestedTestClass _itemInternal;
        private PrivateNestedTestClass _itemPrivate;
        private PublicNestedTestClass _itemPublic;
        private WeakReference _reference;

        [Test]
        public void TestInternalNestedClassAnonymousMethod()
        {
            Reset();

            const int index = 99;

            _itemInternal = new InternalNestedTestClass(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + index, 
                InternalNestedTestClass.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalNestedClassAnonymousStaticMethod()
        {
            Reset();

            _itemInternal = new InternalNestedTestClass();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected, 
                InternalNestedTestClass.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalNestedClassInternalNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemInternal = new InternalNestedTestClass(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.Internal + index, 
                InternalNestedTestClass.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalNestedClassInternalStaticMethod()
        {
            Reset();

            _itemInternal = new InternalNestedTestClass();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.InternalStatic, 
                InternalNestedTestClass.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalNestedClassPrivateNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemInternal = new InternalNestedTestClass(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.Private + index, 
                InternalNestedTestClass.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalNestedClassPrivateStaticMethod()
        {
            Reset();

            _itemInternal = new InternalNestedTestClass();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.PrivateStatic, 
                InternalNestedTestClass.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalNestedClassPublicNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemInternal = new InternalNestedTestClass(index);

            _action = _itemInternal.GetAction(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemInternal);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.Public + index, 
                InternalNestedTestClass.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalNestedClassPublicStaticMethod()
        {
            Reset();

            _itemInternal = new InternalNestedTestClass();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.PublicStatic, 
                InternalNestedTestClass.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateNestedClassAnonymousMethod()
        {
            Reset();

            const int index = 99;

            _itemPrivate = new PrivateNestedTestClass(index);
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + index, 
                PrivateNestedTestClass.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateNestedClassAnonymousStaticMethod()
        {
            Reset();

            _itemPrivate = new PrivateNestedTestClass();
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected, 
                PrivateNestedTestClass.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateNestedClassInternalNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemPrivate = new PrivateNestedTestClass(index);
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.Internal + index, 
                PrivateNestedTestClass.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateNestedClassInternalStaticMethod()
        {
            Reset();

            _itemPrivate = new PrivateNestedTestClass();
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.InternalStatic, 
                PrivateNestedTestClass.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateNestedClassPrivateNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemPrivate = new PrivateNestedTestClass(index);
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.Private + index, 
                PrivateNestedTestClass.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateNestedClassPrivateStaticMethod()
        {
            Reset();

            _itemPrivate = new PrivateNestedTestClass();
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.PrivateStatic, 
                PrivateNestedTestClass.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateNestedClassPublicNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemPrivate = new PrivateNestedTestClass(index);

            _action = _itemPrivate.GetAction(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemPrivate);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.Public + index, 
                PrivateNestedTestClass.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateNestedClassPublicStaticMethod()
        {
            Reset();

            _itemPrivate = new PrivateNestedTestClass();
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.PublicStatic, 
                PrivateNestedTestClass.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicNestedClassAnonymousMethod()
        {
            Reset();

            const int index = 99;

            _itemPublic = new PublicNestedTestClass(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + index, 
                PublicNestedTestClass.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicNestedClassAnonymousStaticMethod()
        {
            Reset();

            _itemPublic = new PublicNestedTestClass();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected, 
                PublicNestedTestClass.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicNestedClassInternalNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemPublic = new PublicNestedTestClass(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.Internal + index, 
                PublicNestedTestClass.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicNestedClassInternalStaticMethod()
        {
            Reset();

            _itemPublic = new PublicNestedTestClass();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.InternalStatic, 
                PublicNestedTestClass.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicNestedClassPrivateNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemPublic = new PublicNestedTestClass(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.Private + index, 
                PublicNestedTestClass.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicNestedClassPrivateStaticMethod()
        {
            Reset();

            _itemPublic = new PublicNestedTestClass();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.PrivateStatic, 
                PublicNestedTestClass.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicNestedClassPublicNamedMethod()
        {
            Reset();

            const int index = 99;

            _itemPublic = new PublicNestedTestClass(index);

            _action = _itemPublic.GetAction(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemPublic);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.Public + index, 
                PublicNestedTestClass.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicNestedClassPublicStaticMethod()
        {
            Reset();

            _itemPublic = new PublicNestedTestClass();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.PublicStatic, 
                PublicNestedTestClass.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        private void Reset()
        {
            _itemPublic = null;
            _itemInternal = null;
            _itemPrivate = null;
            _reference = null;
        }

        public class PublicNestedTestClass
        {
            public const string Expected = "Hello";
            public const string Internal = "Internal";
            public const string InternalStatic = "InternalStatic";
            public const string Private = "Private";
            public const string PrivateStatic = "PrivateStatic";
            public const string Public = "Public";
            public const string PublicStatic = "PublicStatic";
            private readonly int _index; // Just here to force instance methods

            public PublicNestedTestClass()
            {
            }

            public PublicNestedTestClass(int index)
            {
                _index = index;
            }

            public static string Result { get; private set; }

            public static void DoStuffPublicallyAndStatically()
            {
                Result = Expected + PublicStatic;
            }

            public void DoStuffPublically()
            {
                Result = Expected + Public + _index;
            }

            public WeakAction GetAction(WeakActionTestCase testCase)
            {
                WeakAction action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPublically);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffInternally);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPrivately);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPublicallyAndStatically);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffInternallyAndStatically);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPrivatelyAndStatically);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakAction(
                            this, 
                            () => Result = Expected);
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakAction(
                            this, 
                            () => Result = Expected + _index);
                        break;
                }

                return action;
            }

            internal static void DoStuffInternallyAndStatically()
            {
                Result = Expected + InternalStatic;
            }

            internal void DoStuffInternally()
            {
                Result = Expected + Internal + _index;
            }

            private static void DoStuffPrivatelyAndStatically()
            {
                Result = Expected + PrivateStatic;
            }

            private void DoStuffPrivately()
            {
                Result = Expected + Private + _index;
            }
        }

        internal class InternalNestedTestClass
        {
            public const string Expected = "Hello";
            public const string Internal = "Internal";
            public const string InternalStatic = "InternalStatic";
            public const string Private = "Private";
            public const string PrivateStatic = "PrivateStatic";
            public const string Public = "Public";
            public const string PublicStatic = "PublicStatic";
            private readonly int _index; // Just here to force instance methods

            public InternalNestedTestClass()
            {
            }

            public InternalNestedTestClass(int index)
            {
                _index = index;
            }

            public static string Result { get; private set; }

            public static void DoStuffPublicallyAndStatically()
            {
                Result = Expected + PublicStatic;
            }

            public void DoStuffPublically()
            {
                Result = Expected + Public + _index;
            }

            public WeakAction GetAction(WeakActionTestCase testCase)
            {
                WeakAction action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPublically);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffInternally);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPrivately);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPublicallyAndStatically);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffInternallyAndStatically);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPrivatelyAndStatically);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakAction(
                            this, 
                            () => Result = Expected);
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakAction(
                            this, 
                            () => Result = Expected + _index);
                        break;
                }

                return action;
            }

            internal static void DoStuffInternallyAndStatically()
            {
                Result = Expected + InternalStatic;
            }

            internal void DoStuffInternally()
            {
                Result = Expected + Internal + _index;
            }

            private static void DoStuffPrivatelyAndStatically()
            {
                Result = Expected + PrivateStatic;
            }

            private void DoStuffPrivately()
            {
                Result = Expected + Private + _index;
            }
        }

        private class PrivateNestedTestClass
        {
            public const string Expected = "Hello";
            public const string Internal = "Internal";
            public const string InternalStatic = "InternalStatic";
            public const string Private = "Private";
            public const string PrivateStatic = "PrivateStatic";
            public const string Public = "Public";
            public const string PublicStatic = "PublicStatic";
            private readonly int _index; // Just here to force instance methods

            public PrivateNestedTestClass()
            {
            }

            public PrivateNestedTestClass(int index)
            {
                _index = index;
            }

            public static string Result { get; private set; }

            public static void DoStuffPublicallyAndStatically()
            {
                Result = Expected + PublicStatic;
            }

            public void DoStuffPublically()
            {
                Result = Expected + Public + _index;
            }

            public WeakAction GetAction(WeakActionTestCase testCase)
            {
                WeakAction action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPublically);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffInternally);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPrivately);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPublicallyAndStatically);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffInternallyAndStatically);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakAction(
                            this, 
                            DoStuffPrivatelyAndStatically);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakAction(
                            this, 
                            () => Result = Expected);
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakAction(
                            this, 
                            () => Result = Expected + _index);
                        break;
                }

                return action;
            }

            internal static void DoStuffInternallyAndStatically()
            {
                Result = Expected + InternalStatic;
            }

            internal void DoStuffInternally()
            {
                Result = Expected + Internal + _index;
            }

            private static void DoStuffPrivatelyAndStatically()
            {
                Result = Expected + PrivateStatic;
            }

            private void DoStuffPrivately()
            {
                Result = Expected + Private + _index;
            }
        }
    }
}
