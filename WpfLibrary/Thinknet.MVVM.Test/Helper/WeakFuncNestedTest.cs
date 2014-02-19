namespace Thinknet.MVVM.Test.Helper
{
    using System;

    using NUnit.Framework;

    using Thinknet.MVVM.Helper;
    using Thinknet.MVVM.Test.Helper.TestHelperClasses;

    [TestFixture]
    public class WeakFuncNestedTest
    {
        private WeakFunc<string> _action;
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

            _action = _itemInternal.GetFunc(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + index, 
                InternalNestedTestClass.Result);
            Assert.AreEqual(
                InternalNestedTestClass.Expected + index, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected, 
                InternalNestedTestClass.Result);
            Assert.AreEqual(
                InternalNestedTestClass.Expected, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.Internal + index, 
                InternalNestedTestClass.Result);
            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.Internal + index, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.InternalStatic, 
                InternalNestedTestClass.Result);
            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.InternalStatic, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.Private + index, 
                InternalNestedTestClass.Result);
            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.Private + index, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.PrivateStatic, 
                InternalNestedTestClass.Result);
            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.PrivateStatic, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemInternal);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.Public + index, 
                InternalNestedTestClass.Result);
            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.Public + index, 
                result);

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

            _action = _itemInternal.GetFunc(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.PublicStatic, 
                InternalNestedTestClass.Result);
            Assert.AreEqual(
                InternalNestedTestClass.Expected + InternalNestedTestClass.PublicStatic, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + index, 
                PrivateNestedTestClass.Result);
            Assert.AreEqual(
                PrivateNestedTestClass.Expected + index, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected, 
                PrivateNestedTestClass.Result);
            Assert.AreEqual(
                PrivateNestedTestClass.Expected, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.Internal + index, 
                PrivateNestedTestClass.Result);
            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.Internal + index, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.InternalStatic, 
                PrivateNestedTestClass.Result);
            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.InternalStatic, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.Private + index, 
                PrivateNestedTestClass.Result);
            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.Private + index, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.PrivateStatic, 
                PrivateNestedTestClass.Result);
            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.PrivateStatic, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemPrivate);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.Public + index, 
                PrivateNestedTestClass.Result);
            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.Public + index, 
                result);

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

            _action = _itemPrivate.GetFunc(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.PublicStatic, 
                PrivateNestedTestClass.Result);
            Assert.AreEqual(
                PrivateNestedTestClass.Expected + PrivateNestedTestClass.PublicStatic, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.AnonymousMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + index, 
                PublicNestedTestClass.Result);
            Assert.AreEqual(
                PublicNestedTestClass.Expected + index, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.AnonymousStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected, 
                PublicNestedTestClass.Result);
            Assert.AreEqual(
                PublicNestedTestClass.Expected, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.InternalNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.Internal + index, 
                PublicNestedTestClass.Result);
            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.Internal + index, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.InternalStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.InternalStatic, 
                PublicNestedTestClass.Result);
            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.InternalStatic, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.PrivateNamedMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.Private + index, 
                PublicNestedTestClass.Result);
            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.Private + index, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.PrivateStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.PrivateStatic, 
                PublicNestedTestClass.Result);
            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.PrivateStatic, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.PublicNamedMethod);

            _reference = new WeakReference(_itemPublic);
            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.Public + index, 
                PublicNestedTestClass.Result);
            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.Public + index, 
                result);

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

            _action = _itemPublic.GetFunc(WeakActionTestCase.PublicStaticMethod);

            Assert.IsTrue(_reference.IsAlive);
            Assert.IsTrue(_action.IsAlive);

            string result = _action.Execute();

            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.PublicStatic, 
                PublicNestedTestClass.Result);
            Assert.AreEqual(
                PublicNestedTestClass.Expected + PublicNestedTestClass.PublicStatic, 
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

            public static string DoStuffPublicallyAndStaticallyWithResult()
            {
                Result = Expected + PublicStatic;
                return Result;
            }

            public void DoStuffPublically()
            {
                Result = Expected + Public + _index;
            }

            public string DoStuffPublicallyWithResult()
            {
                Result = Expected + Public + _index;
                return Result;
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

            public WeakFunc<string> GetFunc(WeakActionTestCase testCase)
            {
                WeakFunc<string> action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPublicallyWithResult);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffInternallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPrivatelyWithResult);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPublicallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffInternallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPrivatelyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            () =>
                                {
                                    Result = Expected;
                                    return Result;
                                });
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakFunc<string>(
                            this, 
                            () =>
                                {
                                    Result = Expected + _index;
                                    return Result;
                                });
                        break;
                }

                return action;
            }

            internal static void DoStuffInternallyAndStatically()
            {
                Result = Expected + InternalStatic;
            }

            internal static string DoStuffInternallyAndStaticallyWithResult()
            {
                Result = Expected + InternalStatic;
                return Result;
            }

            internal void DoStuffInternally()
            {
                Result = Expected + Internal + _index;
            }

            internal string DoStuffInternallyWithResult()
            {
                Result = Expected + Internal + _index;
                return Result;
            }

            private static void DoStuffPrivatelyAndStatically()
            {
                Result = Expected + PrivateStatic;
            }

            private static string DoStuffPrivatelyAndStaticallyWithResult()
            {
                Result = Expected + PrivateStatic;
                return Result;
            }

            private void DoStuffPrivately()
            {
                Result = Expected + Private + _index;
            }

            private string DoStuffPrivatelyWithResult()
            {
                Result = Expected + Private + _index;
                return Result;
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

            public static string DoStuffPublicallyAndStaticallyWithResult()
            {
                Result = Expected + PublicStatic;
                return Result;
            }

            public void DoStuffPublically()
            {
                Result = Expected + Public + _index;
            }

            public string DoStuffPublicallyWithResult()
            {
                Result = Expected + Public + _index;
                return Result;
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

            public WeakFunc<string> GetFunc(WeakActionTestCase testCase)
            {
                WeakFunc<string> action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPublicallyWithResult);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffInternallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPrivatelyWithResult);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPublicallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffInternallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPrivatelyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            () =>
                                {
                                    Result = Expected;
                                    return Result;
                                });
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakFunc<string>(
                            this, 
                            () =>
                                {
                                    Result = Expected + _index;
                                    return Result;
                                });
                        break;
                }

                return action;
            }

            internal static string DoStuffInternallyAndStaticallyWithResult()
            {
                Result = Expected + InternalStatic;
                return Result;
            }

            internal void DoStuffInternally()
            {
                Result = Expected + Internal + _index;
            }

            internal string DoStuffInternallyWithResult()
            {
                Result = Expected + Internal + _index;
                return Result;
            }

            private static void DoStuffInternallyAndStatically()
            {
                Result = Expected + InternalStatic;
            }

            private static void DoStuffPrivatelyAndStatically()
            {
                Result = Expected + PrivateStatic;
            }

            private static string DoStuffPrivatelyAndStaticallyWithResult()
            {
                Result = Expected + PrivateStatic;
                return Result;
            }

            private void DoStuffPrivately()
            {
                Result = Expected + Private + _index;
            }

            private string DoStuffPrivatelyWithResult()
            {
                Result = Expected + Private + _index;
                return Result;
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

            public static string DoStuffPublicallyAndStaticallyWithResult()
            {
                Result = Expected + PublicStatic;
                return Result;
            }

            public void DoStuffPublically()
            {
                Result = Expected + Public + _index;
            }

            public string DoStuffPublicallyWithResult()
            {
                Result = Expected + Public + _index;
                return Result;
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

            public WeakFunc<string> GetFunc(WeakActionTestCase testCase)
            {
                WeakFunc<string> action = null;

                switch (testCase)
                {
                    case WeakActionTestCase.PublicNamedMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPublicallyWithResult);
                        break;
                    case WeakActionTestCase.InternalNamedMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffInternallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateNamedMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPrivatelyWithResult);
                        break;
                    case WeakActionTestCase.PublicStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPublicallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.InternalStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffInternallyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.PrivateStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            DoStuffPrivatelyAndStaticallyWithResult);
                        break;
                    case WeakActionTestCase.AnonymousStaticMethod:
                        action = new WeakFunc<string>(
                            this, 
                            () =>
                                {
                                    Result = Expected;
                                    return Result;
                                });
                        break;
                    case WeakActionTestCase.AnonymousMethod:
                        action = new WeakFunc<string>(
                            this, 
                            () =>
                                {
                                    Result = Expected + _index;
                                    return Result;
                                });
                        break;
                }

                return action;
            }

            internal static string DoStuffInternallyAndStaticallyWithResult()
            {
                Result = Expected + InternalStatic;
                return Result;
            }

            internal void DoStuffInternally()
            {
                Result = Expected + Internal + _index;
            }

            internal string DoStuffInternallyWithResult()
            {
                Result = Expected + Internal + _index;
                return Result;
            }

            private static void DoStuffPrivatelyAndStatically()
            {
                Result = Expected + PrivateStatic;
            }

            private static string DoStuffPrivatelyAndStaticallyWithResult()
            {
                Result = Expected + PrivateStatic;
                return Result;
            }

            private void DoStuffPrivately()
            {
                Result = Expected + Private + _index;
            }

            private string DoStuffPrivatelyWithResult()
            {
                Result = Expected + Private + _index;
                return Result;
            }
        }
    }
}
