namespace Thinknet.MVVM.Test.Helper
{
    using System;

    using NUnit.Framework;

    using Thinknet.MVVM.Helper;
    using Thinknet.MVVM.Test.Helper.TestHelperClasses;

    [TestFixture]
    public class WeakFuncGenericNestedTest
    {
        private WeakFunc<string, string> _action;
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

            _action = _itemInternal.GetFunc(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + index + parameter, 
                InternalNestedTestClass<string>.Result);
            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + index + parameter, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + parameter, 
                InternalNestedTestClass<string>.Result);
            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + parameter, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.Internal + index + parameter, 
                InternalNestedTestClass<string>.Result);
            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.Internal + index + parameter, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.InternalStatic + parameter, 
                InternalNestedTestClass<string>.Result);
            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.InternalStatic + parameter, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.Private + index + parameter, 
                InternalNestedTestClass<string>.Result);
            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.Private + index + parameter, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.PrivateStatic + parameter, 
                InternalNestedTestClass<string>.Result);
            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.PrivateStatic + parameter, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemInternal);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.Public + index + parameter, 
                InternalNestedTestClass<string>.Result);
            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.Public + index + parameter, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.PublicStatic + parameter, 
                InternalNestedTestClass<string>.Result);
            Assert.AreEqual(
                InternalNestedTestClass<string>.Expected + InternalNestedTestClass<string>.PublicStatic + parameter, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + index + parameter, 
                PrivateNestedTestClass<string>.Result);
            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + index + parameter, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + parameter, 
                PrivateNestedTestClass<string>.Result);
            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + parameter, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.Internal + index + parameter, 
                PrivateNestedTestClass<string>.Result);
            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.Internal + index + parameter, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.InternalStatic + parameter, 
                PrivateNestedTestClass<string>.Result);
            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.InternalStatic + parameter, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.Private + index + parameter, 
                PrivateNestedTestClass<string>.Result);
            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.Private + index + parameter, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.PrivateStatic + parameter, 
                PrivateNestedTestClass<string>.Result);
            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.PrivateStatic + parameter, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemPrivate);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.Public + index + parameter, 
                PrivateNestedTestClass<string>.Result);
            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.Public + index + parameter, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.PublicStatic + parameter, 
                PrivateNestedTestClass<string>.Result);
            Assert.AreEqual(
                PrivateNestedTestClass<string>.Expected + PrivateNestedTestClass<string>.PublicStatic + parameter, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + index + parameter, 
                PublicNestedTestClass<string>.Result);
            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + index + parameter, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + parameter, 
                PublicNestedTestClass<string>.Result);
            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + parameter, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.Internal + index + parameter, 
                PublicNestedTestClass<string>.Result);
            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.Internal + index + parameter, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.InternalStatic + parameter, 
                PublicNestedTestClass<string>.Result);
            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.InternalStatic + parameter, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.Private + index + parameter, 
                PublicNestedTestClass<string>.Result);
            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.Private + index + parameter, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.PrivateStatic + parameter, 
                PublicNestedTestClass<string>.Result);
            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.PrivateStatic + parameter, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemPublic);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.Public + index + parameter, 
                PublicNestedTestClass<string>.Result);
            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.Public + index + parameter, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute(parameter);

            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.PublicStatic + parameter, 
                PublicNestedTestClass<string>.Result);
            Assert.AreEqual(
                PublicNestedTestClass<string>.Expected + PublicNestedTestClass<string>.PublicStatic + parameter, 
                result);

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

            public static string DoStuffPublicallyAndStaticallyWithResult(T parameter)
            {
                Result = Expected + PublicStatic + parameter;
                return Result;
            }

            public void DoStuffPublically(T parameter)
            {
                Result = Expected + Public + _index + parameter;
            }

            public string DoStuffPublicallyWithResult(T parameter)
            {
                Result = Expected + Public + _index + parameter;
                return Result;
            }

            public WeakFunc<T, string> GetFunc(WeakActionTestCase testCase)
            {
                WeakFunc<T, string> action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPublicallyWithResult);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffInternallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPrivatelyWithResult);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPublicallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffInternallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPrivatelyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            p =>
                                {
                                    Result = Expected + p;
                                    return Result;
                                });
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            p =>
                                {
                                    Result = Expected + _index + p;
                                    return Result;
                                });
                        break;
                }

                return action;
            }

            internal static void DoStuffInternallyAndStatically(T parameter)
            {
                Result = Expected + InternalStatic + parameter;
            }

            internal static string DoStuffInternallyAndStaticallyWithResult(T parameter)
            {
                Result = Expected + InternalStatic + parameter;
                return Result;
            }

            internal void DoStuffInternally(T parameter)
            {
                Result = Expected + Internal + _index + parameter;
            }

            internal string DoStuffInternallyWithResult(T parameter)
            {
                Result = Expected + Internal + _index + parameter;
                return Result;
            }

            private static void DoStuffPrivatelyAndStatically(T parameter)
            {
                Result = Expected + PrivateStatic + parameter;
            }

            private static string DoStuffPrivatelyAndStaticallyWithResult(T parameter)
            {
                Result = Expected + PrivateStatic + parameter;
                return Result;
            }

            private void DoStuffPrivately(T parameter)
            {
                Result = Expected + Private + _index + parameter;
            }

            private string DoStuffPrivatelyWithResult(T parameter)
            {
                Result = Expected + Private + _index + parameter;
                return Result;
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

            public static string DoStuffPublicallyAndStaticallyWithResult(T parameter)
            {
                Result = Expected + PublicStatic + parameter;
                return Result;
            }

            public string DoStuffPublicallyWithResult(T parameter)
            {
                Result = Expected + Public + _index + parameter;
                return Result;
            }

            public WeakFunc<T, string> GetFunc(WeakActionTestCase testCase)
            {
                WeakFunc<T, string> action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPublicallyWithResult);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffInternallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPrivatelyWithResult);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPublicallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPrivatelyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffInternallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            p =>
                                {
                                    Result = Expected + p;
                                    return Result;
                                });
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            p =>
                                {
                                    Result = Expected + _index + p;
                                    return Result;
                                });
                        break;
                }

                return action;
            }

            internal string DoStuffInternallyWithResult(T parameter)
            {
                Result = Expected + Internal + _index + parameter;
                return Result;
            }

            private static string DoStuffInternallyAndStaticallyWithResult(T parameter)
            {
                Result = Expected + InternalStatic + parameter;
                return Result;
            }

            private static string DoStuffPrivatelyAndStaticallyWithResult(T parameter)
            {
                Result = Expected + PrivateStatic + parameter;
                return Result;
            }

            private string DoStuffPrivatelyWithResult(T parameter)
            {
                Result = Expected + Private + _index + parameter;
                return Result;
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

            public static string DoStuffPublicallyAndStaticallyWithResult(T parameter)
            {
                Result = Expected + PublicStatic + parameter;
                return Result;
            }

            public string DoStuffPublicallyWithResult(T parameter)
            {
                Result = Expected + Public + _index + parameter;
                return Result;
            }

            public WeakFunc<T, string> GetFunc(WeakActionTestCase testCase)
            {
                WeakFunc<T, string> action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPublicallyWithResult);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffInternallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPrivatelyWithResult);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPublicallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffPrivatelyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            DoStuffInternallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            p =>
                                {
                                    Result = Expected + p;
                                    return Result;
                                });
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakFunc<T, string>(
                            this, 
                            p =>
                                {
                                    Result = Expected + _index + p;
                                    return Result;
                                });
                        break;
                }

                return action;
            }

            internal string DoStuffInternallyWithResult(T parameter)
            {
                Result = Expected + Internal + _index + parameter;
                return Result;
            }

            private static string DoStuffInternallyAndStaticallyWithResult(T parameter)
            {
                Result = Expected + InternalStatic + parameter;
                return Result;
            }

            private static string DoStuffPrivatelyAndStaticallyWithResult(T parameter)
            {
                Result = Expected + PrivateStatic + parameter;
                return Result;
            }

            private string DoStuffPrivatelyWithResult(T parameter)
            {
                Result = Expected + Private + _index + parameter;
                return Result;
            }
        }
    }
}
