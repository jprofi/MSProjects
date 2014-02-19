namespace Thinknet.MVVM.Test.Helper
{
    using System;

    using NUnit.Framework;

    using Thinknet.MVVM.Helper;
    using Thinknet.MVVM.Test.Helper.TestHelperClasses;

    [TestFixture]
    public class WeakActionGenericNestedTest
    {
        private WeakAction<string> _action;
        private InternalNestedTestClass<string> _itemInternal;
        private PrivateNestedTestClass<string> _itemPrivate;
        private PublicNestedTestClass<string> _itemPublic;
        private WeakReference _reference;

        [Test]
        public void TestInternalClassAnonymousMethod()
        {
            Reset();

            const int index = 99;
            const string parameter = "My parameter";

            _itemInternal = new InternalNestedTestClass<string>(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + index + parameter, 
                InternalNestedTestClass<string>.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassAnonymousStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemInternal = new InternalNestedTestClass<string>();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + parameter, 
                InternalNestedTestClass<string>.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassInternalNamedMethod()
        {
            Reset();

            const string parameter = "My parameter";
            const int index = 99;

            _itemInternal = new InternalNestedTestClass<string>(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.Internal + index + parameter, 
                InternalNestedTestClass<string>.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassInternalStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemInternal = new InternalNestedTestClass<string>();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.InternalStatic + parameter, 
                InternalNestedTestClass<string>.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassPrivateNamedMethod()
        {
            Reset();

            const string parameter = "My parameter";
            const int index = 99;

            _itemInternal = new InternalNestedTestClass<string>(index);
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.Private + index + parameter, 
                InternalNestedTestClass<string>.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassPrivateStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemInternal = new InternalNestedTestClass<string>();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.PrivateStatic + parameter, 
                InternalNestedTestClass<string>.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassPublicNamedMethod()
        {
            Reset();

            const int index = 99;
            const string parameter = "My parameter";

            _itemInternal = new InternalNestedTestClass<string>(index);

            _action = _itemInternal.GetAction(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemInternal);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.Public + index + parameter, 
                InternalNestedTestClass<string>.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestInternalClassPublicStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemInternal = new InternalNestedTestClass<string>();
            _reference = new WeakReference(_itemInternal);

            _action = _itemInternal.GetAction(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.PublicStatic + parameter, 
                InternalNestedTestClass<string>.Result);

            _itemInternal = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateClassAnonymousMethod()
        {
            Reset();

            const int index = 99;
            const string parameter = "My parameter";

            _itemPrivate = new PrivateNestedTestClass<string>(index);
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + index + parameter, 
                PrivateNestedTestClass<string>.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateClassAnonymousStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPrivate = new PrivateNestedTestClass<string>();
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + parameter, 
                PrivateNestedTestClass<string>.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateClassInternalNamedMethod()
        {
            Reset();

            const string parameter = "My parameter";
            const int index = 99;

            _itemPrivate = new PrivateNestedTestClass<string>(index);
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.Internal + index + parameter, 
                PrivateNestedTestClass<string>.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateClassInternalStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPrivate = new PrivateNestedTestClass<string>();
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.InternalStatic + parameter, 
                PrivateNestedTestClass<string>.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateClassPrivateNamedMethod()
        {
            Reset();

            const string parameter = "My parameter";
            const int index = 99;

            _itemPrivate = new PrivateNestedTestClass<string>(index);
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.Private + index + parameter, 
                PrivateNestedTestClass<string>.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateClassPrivateStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPrivate = new PrivateNestedTestClass<string>();
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.PrivateStatic + parameter, 
                PrivateNestedTestClass<string>.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateClassPublicNamedMethod()
        {
            Reset();

            const int index = 99;
            const string parameter = "My parameter";

            _itemPrivate = new PrivateNestedTestClass<string>(index);

            _action = _itemPrivate.GetAction(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemPrivate);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.Public + index + parameter, 
                PrivateNestedTestClass<string>.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPrivateClassPublicStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPrivate = new PrivateNestedTestClass<string>();
            _reference = new WeakReference(_itemPrivate);

            _action = _itemPrivate.GetAction(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.PublicStatic + parameter, 
                PrivateNestedTestClass<string>.Result);

            _itemPrivate = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassAnonymousMethod()
        {
            Reset();

            const int index = 99;
            const string parameter = "My parameter";

            _itemPublic = new PublicNestedTestClass<string>(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + index + parameter, 
                PublicNestedTestClass<string>.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassAnonymousStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPublic = new PublicNestedTestClass<string>();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + parameter, 
                PublicNestedTestClass<string>.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassInternalNamedMethod()
        {
            Reset();

            const string parameter = "My parameter";
            const int index = 99;

            _itemPublic = new PublicNestedTestClass<string>(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.Internal + index + parameter, 
                PublicNestedTestClass<string>.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassInternalStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPublic = new PublicNestedTestClass<string>();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.InternalStatic + parameter, 
                PublicNestedTestClass<string>.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassPrivateNamedMethod()
        {
            Reset();

            const string parameter = "My parameter";
            const int index = 99;

            _itemPublic = new PublicNestedTestClass<string>(index);
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.Private + index + parameter, 
                PublicNestedTestClass<string>.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassPrivateStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPublic = new PublicNestedTestClass<string>();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.PrivateStatic + parameter, 
                PublicNestedTestClass<string>.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassPublicNamedMethod()
        {
            Reset();

            const int index = 99;
            const string parameter = "My parameter";

            _itemPublic = new PublicNestedTestClass<string>(index);

            _action = _itemPublic.GetAction(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemPublic);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.Public + index + parameter, 
                PublicNestedTestClass<string>.Result);

            _itemPublic = null;
            GC.Collect();

            Assert.IsFalse(_reference.IsAlive);
        }

        [Test]
        public void TestPublicClassPublicStaticMethod()
        {
            Reset();

            const string parameter = "My parameter";

            _itemPublic = new PublicNestedTestClass<string>();
            _reference = new WeakReference(_itemPublic);

            _action = _itemPublic.GetAction(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.PublicStatic + parameter, 
                PublicNestedTestClass<string>.Result);

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

        public class PublicNestedTestClass<T>
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

            public static void DoStuffPublicallyAndStatically(T parameter)
            {
                Result = Expected + PublicStatic + parameter;
            }

            public void DoStuffPublically(T parameter)
            {
                Result = Expected + Public + _index + parameter;
            }

            public WeakAction<T> GetAction(WeakActionTestCase testCase)
            {
                WeakAction<T> action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPublically);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffInternally);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPrivately);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPublicallyAndStatically);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffInternallyAndStatically);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPrivatelyAndStatically);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            p => Result = Expected + p);
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakAction<T>(
                            this, 
                            p => Result = Expected + _index + p);
                        break;
                }

                return action;
            }

            internal static void DoStuffInternallyAndStatically(T parameter)
            {
                Result = Expected + InternalStatic + parameter;
            }

            internal void DoStuffInternally(T parameter)
            {
                Result = Expected + Internal + _index + parameter;
            }

            private static void DoStuffPrivatelyAndStatically(T parameter)
            {
                Result = Expected + PrivateStatic + parameter;
            }

            private void DoStuffPrivately(T parameter)
            {
                Result = Expected + Private + _index + parameter;
            }
        }

        internal class InternalNestedTestClass<T>
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

            public static void DoStuffPublicallyAndStatically(T parameter)
            {
                Result = Expected + PublicStatic + parameter;
            }

            public void DoStuffPublically(T parameter)
            {
                Result = Expected + Public + _index + parameter;
            }

            public WeakAction<T> GetAction(WeakActionTestCase testCase)
            {
                WeakAction<T> action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPublically);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffInternally);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPrivately);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPublicallyAndStatically);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPrivatelyAndStatically);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffInternallyAndStatically);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            p => Result = Expected + p);
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakAction<T>(
                            this, 
                            p => Result = Expected + _index + p);
                        break;
                }

                return action;
            }

            internal void DoStuffInternally(T parameter)
            {
                Result = Expected + Internal + _index + parameter;
            }

            private static void DoStuffInternallyAndStatically(T parameter)
            {
                Result = Expected + InternalStatic + parameter;
            }

            private static void DoStuffPrivatelyAndStatically(T parameter)
            {
                Result = Expected + PrivateStatic + parameter;
            }

            private void DoStuffPrivately(T parameter)
            {
                Result = Expected + Private + _index + parameter;
            }
        }

        private class PrivateNestedTestClass<T>
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

            public static void DoStuffPublicallyAndStatically(T parameter)
            {
                Result = Expected + PublicStatic + parameter;
            }

            public void DoStuffPublically(T parameter)
            {
                Result = Expected + Public + _index + parameter;
            }

            public WeakAction<T> GetAction(WeakActionTestCase testCase)
            {
                WeakAction<T> action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPublically);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffInternally);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPrivately);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPublicallyAndStatically);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffPrivatelyAndStatically);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            DoStuffInternallyAndStatically);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakAction<T>(
                            this, 
                            p => Result = Expected + p);
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakAction<T>(
                            this, 
                            p => Result = Expected + _index + p);
                        break;
                }

                return action;
            }

            internal void DoStuffInternally(T parameter)
            {
                Result = Expected + Internal + _index + parameter;
            }

            private static void DoStuffInternallyAndStatically(T parameter)
            {
                Result = Expected + InternalStatic + parameter;
            }

            private static void DoStuffPrivatelyAndStatically(T parameter)
            {
                Result = Expected + PrivateStatic + parameter;
            }

            private void DoStuffPrivately(T parameter)
            {
                Result = Expected + Private + _index + parameter;
            }
        }
    }
}
